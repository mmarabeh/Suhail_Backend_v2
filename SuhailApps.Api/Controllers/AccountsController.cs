using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuhailApps.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuhailApps.Api.Controllers
{
    //[Route("api/[Accounts]")]
    [ApiController]
    public class AccountsController : BaseController
    {
        #region Private Variables

        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Public Constructers

        public AccountsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #endregion

        #region Apis

        /// <summary>
        /// generate new token with expiry time from firebase .
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RefreshToken(string token)
        {
            var result = await _authenticationService.RefreshToken(token).ConfigureAwait(false);
            return await GetResponse(result);
        }

        #endregion


    }
}
