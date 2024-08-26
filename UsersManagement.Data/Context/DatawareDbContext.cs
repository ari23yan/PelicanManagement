using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Entities.Dataware;
using UsersManagement.Domain.Entities.Pelican;

namespace UsersManagement.Data.Context
{
    public class DatawareDbContext: DbContext
    {
        public DatawareDbContext(DbContextOptions<DatawareDbContext> dbContext) : base(dbContext) { }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}
