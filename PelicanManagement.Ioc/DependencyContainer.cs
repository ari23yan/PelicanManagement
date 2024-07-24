using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Interfaces;
using Shop.Domain.Interfaces.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Data.Repositories;
using Shop.Data.Repositories.User;
using Shop.Data.Repositories.Product;
using Shop.Domain.Interfaces.Product;
using Shop.Application.Services.Interfaces;
using Shop.Application.Services.Implementations;
using Shop.Application.Security;
using Shop.Application.Senders;

namespace Shop.Ioc
{
    public static class DependencyContainer
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ISender, Sender>();
            services.AddScoped<ILogService,  LogService>();
        }
    }
}
