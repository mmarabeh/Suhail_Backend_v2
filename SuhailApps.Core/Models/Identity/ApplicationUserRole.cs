using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SuhailApps.Core.Models.Identity
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public int Id { get; set; }
    }
}
