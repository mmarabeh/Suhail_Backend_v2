using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuhailApps.Core.Interfaces;

namespace SuhailApps.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : BaseController
    {

        #region Private variables

        private readonly IAttachmentService _attachmentService;

        #endregion

        #region Constructers

        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }
        #endregion

        #region Actions
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var result = await _attachmentService.AddAttachment(file);
            return await GetResponse(result);
        }

        /// <summary>
        /// Download attachment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id, [FromQuery] string resizeFactor = "")
        {
            var result = await _attachmentService.GetAttachment(id, resizeFactor);
            return File(result.ResultObj.FileContent, result.ResultObj.FileType);
        }

        #endregion
    }
}
