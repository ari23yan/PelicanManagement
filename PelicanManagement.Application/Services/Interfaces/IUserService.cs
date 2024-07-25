using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Permissions;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<(UserAuthResponse Result, UserDto? user)> LoginUser(AuthenticateDto input);
        Task<ResponseDto<GetRoleMenuDto>> GetRoleMenusByRoleId(Guid roleId);
        Task<ResponseDto<IEnumerable<UsersListDto>>> GetPaginatedUsersList(PaginationDto request);
        Task<ResponseDto<AddUserDto>> AddUser(AddUserDto requset, Guid operatorId);
        Task<ResponseDto<bool>> DeleteUserByUserId(Guid userId, Guid operatorId);
        Task<ResponseDto<UserDetailDto>> GetUserDetailByUserId(Guid userId);
        Task<ResponseDto<IEnumerable<GetRolesListDto>>> GetRolesList();
        Task<ResponseDto<bool>> UpdateUserByUserId(Guid userId, UpdateUserDto request, Guid operatorId);


    }
}
