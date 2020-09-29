using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using SuhailApps.Core.Interfaces;

namespace SuhailApps.Core.Models.Identity
{
    public class ApplicationRole : IdentityRole,IActiveModel
    {

        public bool Active { get; set; } = true;
        public bool Admin { get; set; } = false;
        public string Description { get; set; }
        public string HomePage { get; set; }


        public virtual ICollection<ApplicationRoleClaim> Claims { get; } = new List<ApplicationRoleClaim>();

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
