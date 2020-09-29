using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuhailApps.Core.Classes;
using SuhailApps.Core.ViewModels;

namespace SuhailApps.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {

        [NonAction]
        public async Task<IActionResult> GetResponse<T>(ProcessResult<T> processResult)
        {
            switch (processResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await Task.FromResult(Ok(new SuccessResponseWrapper()
                    {
                        Message = processResult.Message,
                        Data = processResult.ResultObj,
                        Status = processResult.Succeeded,
                    }));
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.InternalServerError:

                    return await Task.FromResult(BadRequest(new FailureResponseWrapper()
                    {
                        Message = processResult.Message,
                        ErrorCode = processResult.ErrorCode,
                        Status = processResult.Succeeded
                    }));
                case HttpStatusCode.NotFound:
                    return await Task.FromResult(NotFound());
                default:
                    return await Task.FromResult(Ok(new SuccessResponseWrapper()
                    {
                        Message = processResult.Message,
                        Data = processResult.ResultObj,
                        Status = processResult.Succeeded,
                    }));

            }

        }
    }
}
