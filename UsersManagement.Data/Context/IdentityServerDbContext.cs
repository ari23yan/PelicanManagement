using Microsoft.EntityFrameworkCore;
using UsersManagement.Domain.Entities.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Data.Context
{
    public class IdentityServerDbContext:DbContext
    {
        public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> dbContext) : base(dbContext) { }
        public DbSet<User> Users { get; set; }
    }   
}
