using Microsoft.EntityFrameworkCore;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PelicanManagement.Data.Context
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
    }
}
