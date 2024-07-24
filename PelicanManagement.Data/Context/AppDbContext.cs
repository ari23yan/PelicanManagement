using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities.Account;
using Shop.Domain.Entities.Common;
using Shop.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext){}

        public DbSet<User> Users  { get; set; }
        public DbSet<UserDetail> UserDetails  { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<ProductDetail> ProductDetails  { get; set; }
        public DbSet<Material> Materials  { get; set; }
        public DbSet<Category> Categories  { get; set; }
        public DbSet<Gallery> Galleries  { get; set; }
        public DbSet<Banner> Banners  { get; set; }
        public DbSet<Blog> Blogs  { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs  { get; set; }
    }
}
