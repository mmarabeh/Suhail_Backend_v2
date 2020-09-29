using SuhailApps.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.Configuration;
using SuhailApps.Core.Classes;
using SuhailApps.Core.ViewModels;

namespace SuhailApps.Core.Services
{
    /// <summary>
    /// The service which is responsible for authenticating users either with Firebase or any other identity servers.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region Private Variables

        private readonly FirebaseClient _fireBaseClient;
        private readonly string _fireBaseUrl;
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructers
        public AuthenticationService(IConfiguration configuration)
        {
            var obj=   FirebaseApp.Create();
            var defaultAuth = FirebaseAuth.GetAuth(obj);
            
            _configuration = configuration;
            _fireBaseUrl = configuration.GetSection("FireBaseUrl").Value;
            _fireBaseClient = new FirebaseClient(_fireBaseUrl);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        public async Task<ProcessResult<string>> AuthenticateUser(string phoneNo)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessResult<TokenViewModel>> RefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public async Task<ProcessResult<string>> VerifyUser(string phoneNo, string verificationCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Helpers

        #endregion

    }
}
