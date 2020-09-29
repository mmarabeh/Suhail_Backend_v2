using Microsoft.EntityFrameworkCore;
using SuhailApps.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using SuhailApps.Core.Models.Identity;

namespace SuhailApps.Core.Data
{
    public class ApplicationDbContext : DbContext
    {


        public DbSet<Offer> Offers { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationRoleClaim> ApplicationRoleClaims { get; set; }

        public DbSet<Attachment>   Attachments { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        { }
    }
}
