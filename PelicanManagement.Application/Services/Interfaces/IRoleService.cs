using PelicanManagement.Domain.Dtos.Common;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Permissions;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<ResponseDto<IEnumerable<RolesListDto>>> GetPaginatedRolesList(PaginationDto request);
        Task<ResponseDto<RoleMenuDto>> GetRoleMenusByRoleId(Guid roleId);
        Task<ResponseDto<IEnumerable<RolesListDto>>> GetRolesList();
        Task<ResponseDto<bool>> AddRole(AddRoleDto requset, Guid operatorId);
        Task<ResponseDto<bool>> UpdateRole(Guid roleId, UpdateRoleDto request, Guid operatorId);
        Task<ResponseDto<bool>> DeleteRoleByRoleId(Guid roleId, Guid operatorId);
        Task<ResponseDto<bool>> ToggleActiveStatusByRoleId(Guid roleId, Guid operatorId);
        Task<ResponseDto<IEnumerable<PermissionsDto>>> GetRolePermissionsByRoleId(GetByIdDto dto);
    }
}
