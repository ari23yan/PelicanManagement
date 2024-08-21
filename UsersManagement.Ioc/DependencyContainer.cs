using Microsoft.Extensions.DependencyInjection;
using UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Data.Repositories;
using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Application.Services.Implementations;
using UsersManagement.Application.Security;
using UsersManagement.Application.Senders;
using UsersManagement.Domain.Interfaces.Management;
using UsersManagement.Data.Repositories.Management;
using Microsoft.AspNetCore.Identity;
using UsersManagement.Domain.Entities.IdentityServer;
using UsersManagement.Data.Context;


namespace UsersManagement.Ioc
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
