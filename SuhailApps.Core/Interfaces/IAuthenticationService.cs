using SuhailApps.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SuhailApps.Core.ViewModels;

namespace SuhailApps.Core.Interfaces
{
    public interface IAuthenticationService
    {

        /// <summary>
        /// Authenticate user by phone no 
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        Task<ProcessResult<string>> AuthenticateUser(string phoneNo);


        /// <summary>
        /// verify user 
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        Task<ProcessResult<string>> VerifyUser(string phoneNo,string verificationCode);


        Task<ProcessResult<TokenViewModel>> RefreshToken(string token);
    }
}
