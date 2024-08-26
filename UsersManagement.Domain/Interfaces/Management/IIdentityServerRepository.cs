using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Entities.IdentityServer;
using UsersManagement.Domain.Entities.Pelican;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Interfaces.GenericRepositories;

namespace UsersManagement.Domain.Interfaces.Management
{
    public interface IIdentityServerRepository : IIdentityServerGenericRepository<Entities.IdentityServer.User>
    {
        Task<ListResponseDto<Entities.IdentityServer.User>> GetPaginatedUsersList(PaginationDto request, UserType type);
        Task<Entities.IdentityServer.User> Get(int userId);
        Task<Entities.IdentityServer.User> GetByUsername(string username);
    }
}
