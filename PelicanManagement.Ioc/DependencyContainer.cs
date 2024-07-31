using Microsoft.Extensions.DependencyInjection;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PelicanManagement.Data.Repositories;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Services.Implementations;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Senders;

namespace PelicanManagement.Ioc
{
    public static class DependencyContainer
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ISender, Sender>();
            services.AddScoped<ILogService,  LogService>();
        }
    }
}
