using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SuhailApps.Core.Interfaces;

namespace SuhailApps.Core.Models.Identity
{
    public class ApplicationUser : IdentityUser,IActiveModel
    {
        //Inherited properties
        //Username, Email, PhoneNumber, ...
        public bool Active { get; set; } = true;
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Photo { get; set; }



        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<ApplicationUserRole> Roles { get; } = new List<ApplicationUserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<ApplicationUserClaim> Claims { get; } = new List<ApplicationUserClaim>();


        /// <summary>
        /// Overrides <see cref="object.ToString()"/> to use <see cref="Newtonsoft.Json.JsonConvert.SerializeObject(object)"/> 
        /// </summary>
        /// <returns>Serialized json string</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}