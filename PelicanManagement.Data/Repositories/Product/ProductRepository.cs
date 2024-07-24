using Shop.Domain.Entities.Product;
using Shop.Domain.Interfaces.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities.Product;
using Shop.Data.Context;

namespace Shop.Data.Repositories.Product
{
    public class ProductRepository : Repository<Shop.Domain.Entities.Product.Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context){}
    }
}
