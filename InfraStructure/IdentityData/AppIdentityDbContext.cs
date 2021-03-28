using Core.PocoEntities.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraStructure.IdentityData
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        //We Will Not Write Any Code Like Dbsets for Identity Tables Bcz,
        // IdentityDbContext Will Manage that by Default.
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
