using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Domain.Entities.Pelican;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces.Management
{
    public interface IIdentityServerRepository : IIdentityServerGenericRepository<Entities.IdentityServer.User>
    {
        Task<ListResponseDto<Entities.IdentityServer.User>> GetPaginatedUsersList(PaginationDto request);
        Task<Entities.IdentityServer.User> Get(int userId);

    }
}
