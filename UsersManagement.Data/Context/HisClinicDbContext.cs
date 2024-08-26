using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Entities.Dataware;
using UsersManagement.Domain.Entities.HisClinic;
using UsersManagement.Domain.Entities.Pelican;

namespace UsersManagement.Data.Context
{
    public class HisClinicDbContext: DbContext
    {
        public HisClinicDbContext(DbContextOptions<HisClinicDbContext> dbContext) : base(dbContext) { }

        public DbSet<ApiUsers> ApiUsers { get; set; }
    }
}
