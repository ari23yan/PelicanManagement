using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Permissions;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseDto<IEnumerable<RolesListDto>>> GetPaginatedRolesList(PaginationDto request);
        Task<ResponseDto<RoleMenuDto>> GetRoleMenusByRoleId(Guid roleId);
        Task<ResponseDto<RolesListWithPermissionAndMenusDto>> GetRolesPermissionAndMenus(Guid? roleId);
        Task<ResponseDto<IEnumerable<RolesListDto>>> GetRolesList();
        Task<ResponseDto<bool>> AddRole(AddRoleDto requset, Guid operatorId);
        Task<ResponseDto<bool>> UpdateRole(Guid roleId, UpdateRoleDto request, Guid operatorId);
        Task<ResponseDto<bool>> DeleteRoleByRoleId(Guid roleId, Guid operatorId);
        Task<ResponseDto<bool>> ToggleActiveStatusByRoleId(Guid roleId, Guid operatorId);
        Task<ResponseDto<IEnumerable<PermissionsDto>>> GetRolePermissionsByRoleId(GetByIdDto dto);
    }
}
