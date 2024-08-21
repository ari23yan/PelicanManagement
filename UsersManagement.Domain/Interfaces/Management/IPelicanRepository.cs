using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Dtos.Management.Pelican;
using UsersManagement.Domain.Entities.Pelican;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Interfaces.Management
{
    public interface IPelicanRepository: IPelicanGenericRepository<ApiUser>
    {
        Task<ListResponseDto<ApiUser>> GetPaginatedUsersList(PaginationDto request);
        Task<ApiUser> Get(string userId);
        Task<ApiUser> GetByUsername(string username);


        Task<List<PelicanUserPermissionsDto>> GetUserPermissionsByUsername(string username);
        Task<List<PelicanUserUnitsDto>> GetUserUnitsByUsername(string username);
        Task<List<UsersUnit>> GetUserUnits(string username);
        Task<List<UserPermission>> GetUserPermissions(string username);
        Task<PermissionsAndUnitsDto> GetPermissionsAndUnits();

    }
}
