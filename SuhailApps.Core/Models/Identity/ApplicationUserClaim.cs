using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SuhailApps.Core.Interfaces;

namespace SuhailApps.Core.Models.Identity
{
    public class ApplicationUserClaim : IdentityUserClaim<string>,IActiveModel
    {
        public bool Active { get; set; }
    }


}