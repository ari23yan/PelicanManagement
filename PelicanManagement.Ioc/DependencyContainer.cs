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
using PelicanManagement.Domain.Interfaces.Management;
using PelicanManagement.Data.Repositories.Management;
using Microsoft.AspNetCore.Identity;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Data.Context;


namespace PelicanManagement.Ioc
{
    public static class DependencyContainer
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IIdentityServerGenericRepository<>), typeof(IdentityServerGenericRepository<>));
            services.AddScoped(typeof(IPelicanGenericRepository<>), typeof(PelicanGenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IManagementService, ManagementService>();
            services.AddScoped<IPelicanRepository, PelicanRepository>();
            services.AddScoped<IIdentityServerRepository, IdentityServerRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ISender, Sender>();
            services.AddScoped<ILogService,  LogService>();
            services.AddScoped<IPasswordHasher<Domain.Entities.IdentityServer.User>, PasswordHasher<Domain.Entities.IdentityServer.User>>();
        }
    }
}
