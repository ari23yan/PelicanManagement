using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Domain.Entities.Pelican;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces.Management
{
    public interface IIdentityServerRepository : IIdentityServerGenericRepository<Entities.IdentityServer.User>
    {
        Task<ListResponseDto<Entities.IdentityServer.User>> GetPaginatedUsersList(PaginationDto request, UserType type);
        Task<Entities.IdentityServer.User> Get(int userId);
        Task<Entities.IdentityServer.User> GetByUsername(string username);
    }
}
