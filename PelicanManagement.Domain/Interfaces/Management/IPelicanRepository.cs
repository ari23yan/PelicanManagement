using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Management.Pelican;
using PelicanManagement.Domain.Entities.Pelican;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces.Management
{
    public interface IPelicanRepository: IPelicanGenericRepository<ApiUser>
    {
        Task<ListResponseDto<ApiUser>> GetPaginatedUsersList(PaginationDto request);
        Task<ApiUser> Get(string userId);


        Task<List<PelicanUserPermissionsDto>> GetUserPermissionsByUsername(string username);
        Task<List<PelicanUserUnitsDto>> GetUserUnitsByUsername(string username);
        Task<List<UsersUnit>> GetUserUnits(string username);
        Task<List<UserPermission>> GetUserPermissions(string username);
        Task<PermissionsAndUnitsDto> GetPermissionsAndUnits();

    }
}
