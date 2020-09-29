using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SuhailApps.Core.Classes;
using SuhailApps.Core.ViewModels;

namespace SuhailApps.Core.Interfaces
{
    public interface IAttachmentService
    {
        /// <summary>
        /// Add attachment.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<ProcessResult<string>> AddAttachment(IFormFile file);

        /// <summary>
        /// Get attachment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resizeFactor"></param>
        /// <returns></returns>
        Task<ProcessResult<AttachmentViewModel>> GetAttachment(string id, string resizeFactor = "");
    }
}
