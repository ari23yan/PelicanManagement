using Microsoft.EntityFrameworkCore;
using PelicanManagement.Domain.Entities.Pelican;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Data.Context
{
    public class PelicanDbContext : DbContext
    {
        public PelicanDbContext(DbContextOptions<PelicanDbContext> options)
            : base(options)
        { }

        public DbSet<ApiUser> ApiUsers { get; set; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<UsersUnit> UsersUnits { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiUser>(entity =>
            {
                entity.ToTable("ApiUsers");
                entity.HasKey(e => e.Id);
                // Configure other properties as needed
            });
        }
    }
}
