using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SuhailApps.Core.Models.Identity
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public bool Active { get; set; }
    }


}