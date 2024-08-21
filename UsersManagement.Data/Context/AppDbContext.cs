using Microsoft.EntityFrameworkCore;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Entities.UsersManagement.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UsersManagement.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext){}
        public DbSet<User> Users  { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs  { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs  { get; set; }
        public DbSet<UserActivityLogType> UserActivityLogTypes  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoleMenu>()
                .HasKey(rm => new { rm.RoleId, rm.MenuId });

            modelBuilder.Entity<RoleMenu>()
                .HasOne(rm => rm.Role)
                .WithMany(r => r.RoleMenus)
                .HasForeignKey(rm => rm.RoleId);

            modelBuilder.Entity<RoleMenu>()
                .HasOne(rm => rm.Menu)
                .WithMany(m => m.RoleMenus)
                .HasForeignKey(rm => rm.MenuId);
        }

    }
}
