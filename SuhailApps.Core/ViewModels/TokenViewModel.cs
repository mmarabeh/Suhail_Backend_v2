using System;
using System.Collections.Generic;
using System.Text;

namespace SuhailApps.Core.ViewModels
{
    public class TokenViewModel
    {

        public string Token { get; set; }


        public string RefreshToken { get; set; }


        public TimeSpan ExpiresIn { get; set; }
    }
}
