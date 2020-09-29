using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SuhailApps.Core.Classes;
using SuhailApps.Core.Enums;
using SuhailApps.Core.Interfaces;
using SuhailApps.Core.ViewModels;

namespace SuhailApps.Core.Services
{
    public class AttachmentService: IAttachmentService
    {
        #region Private Variables

        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constucters

        public AttachmentService(IRepository repository,IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }


        #endregion

        #region Public Method

        /// <summary>
        /// Add attachment file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ProcessResult<string>> AddAttachment(IFormFile file)
        {
            var addAttachmentResult = new ProcessResult<string> {StatusCode = HttpStatusCode.OK, Succeeded = true};
            try
            {
                if (file == null)
                {
                    addAttachmentResult.StatusCode = HttpStatusCode.BadRequest;
                    addAttachmentResult.Succeeded = false;
                    addAttachmentResult.Message = "File can't be null!";
                    return await Task.FromResult(addAttachmentResult).ConfigureAwait(false);
                }

                // Get the attachments folder path to save file in it
                var attachmentsFolderPath = _configuration.GetSection("AttachmentsPath").Value;

                // return the id of saved attachment file
                var attachment = await SaveFile( file, attachmentsFolderPath);
                addAttachmentResult.ResultObj = attachment?.Id.ToString();
            }
            catch (Exception e)
            {
                addAttachmentResult.Succeeded = false;
                addAttachmentResult.Message = e.Message;
                addAttachmentResult.StatusCode = HttpStatusCode.BadRequest;
            }

            return await Task.FromResult(addAttachmentResult).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Attachment file by Id
        /// </summary>
        /// <returns></returns>
        public async Task<ProcessResult<AttachmentViewModel>> GetAttachment(string id, string resizeFactor = "")
        {
            var getAttachmentResult = new ProcessResult<AttachmentViewModel>
                {StatusCode = HttpStatusCode.OK, Succeeded = true};

            Guid attachmentId;

            if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out attachmentId))
            {
                getAttachmentResult.Message = "Invalid AttachmentId";
                getAttachmentResult.Succeeded = false;
                getAttachmentResult.StatusCode = HttpStatusCode.BadRequest;
                return await Task.FromResult(getAttachmentResult).ConfigureAwait(false);
            }

            byte[] fileContent = null;

            try
            {
                var attachment = _repository.Find<Models.Attachment>(a => a.Id == attachmentId);
                if (attachment == null || !File.Exists(attachment.FilePath))
                {
                    getAttachmentResult.Message = "The attachment file not found!";
                    getAttachmentResult.StatusCode = HttpStatusCode.NotFound;
                    getAttachmentResult.Succeeded = false;
                    return await Task.FromResult(getAttachmentResult).ConfigureAwait(false);
                }

                if (!string.IsNullOrWhiteSpace(resizeFactor))
                {
                    fileContent = ProcessImage(attachment.FilePath, resizeFactor);
                    if (fileContent?.Length == 0)
                    {
                        fileContent = await File.ReadAllBytesAsync(attachment.FilePath);
                    }
                }
                else
                    fileContent = await File.ReadAllBytesAsync(attachment.FilePath);

                getAttachmentResult.ResultObj = new AttachmentViewModel
                    {FileContent = fileContent, FileName = attachment.FileName, FileType = attachment.FileType};
            }
            catch (Exception e)
            {
                getAttachmentResult.Succeeded = false;
                getAttachmentResult.Message = e.Message;
                getAttachmentResult.StatusCode = HttpStatusCode.BadRequest;
            }

            return await Task.FromResult(getAttachmentResult).ConfigureAwait(false);
            ;

        }

        #endregion


        #region Private Helpers

        /// <summary>
        /// Save File in File system then log it's reference into attachment table.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<Models.Attachment> SaveFile(IFormFile file, string rootPath,
            string fileId = "")
        {
            if (file == null)
                return null;

            var fileExt = Path.GetExtension(file?.FileName);

            if (string.IsNullOrEmpty(rootPath))
            {
                rootPath = Path.GetTempPath();
            }

            var filePath = Path.Combine(rootPath, Guid.NewGuid() + fileExt);

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream).ConfigureAwait(false);
            }

            var attachment = new Models.Attachment()
            {
                FileName = file.FileName,
                FileType = file.ContentType,
                Extension = fileExt,
                FilePath = filePath,
                FileId = fileId

            };
            _repository.Add(attachment);
            await _repository.SaveChangesAsync();
            return attachment;
        }


        /// <summary>
        /// Resize image to the given path with the given new width
        /// </summary>
        private Image ResizeImage(Image originalImage, int newImageWidth)
        {
            int newWidth = newImageWidth;

            int w = originalImage.Width;
            int h = originalImage.Height;
            int nw = w;
            int nh = h;

            decimal ratio = 0;

            if (newWidth < w)
            {
                ratio = (decimal) w / (decimal) newWidth;
                nw = newWidth;
                nh = (int) (h / ratio);
            }
            else
            {
                ratio = (decimal) newWidth / (decimal) w;
                nw = newWidth;
                nh = (int) (h * ratio);
            }

            Image target = new Bitmap(nw, nh);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, nw, nh);
            }

            return target;
        }

        /// <summary>
        /// read file content
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="resizeFactor"></param>
        /// <returns></returns>
        private byte[] ProcessImage(string filePath, string resizeFactor = "")
        {
            var fileContent = new byte[] { };
            int newWidth = 0;

            try
            {
                // ResizeFactor empty -- no resize
                if (!string.IsNullOrWhiteSpace(resizeFactor) && IsImage(filePath))
                {
                    ImageSize resizeImageWidth = default(ImageSize);
                    Enum.TryParse<ImageSize>(resizeFactor, out resizeImageWidth);
                    newWidth = 0; //ToDo:read from stettings.

                    // Resize file if type = image and given new width
                    Image originalImage = Image.FromFile(filePath, true);
                    Image target = ResizeImage(originalImage, newWidth);
                    // Convert image to byte[] (fileContentResult)
                    MemoryStream ms = new MemoryStream();
                    target.Save(ms, originalImage.RawFormat);
                    fileContent = ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                return fileContent;
            }

            return fileContent;
        }

        /// <summary>
        /// check if file passed is image
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsImage(string filePath)
        {
            try
            {

                using (Image newImage = Image.FromFile(filePath))
                {
                }
            }
            catch (OutOfMemoryException ex)
            {
                //The file does not have a valid image format.
                //-or- GDI+ does not support the pixel format of the file

                return false;
            }

            return true;
        }

        #endregion

    }
}
