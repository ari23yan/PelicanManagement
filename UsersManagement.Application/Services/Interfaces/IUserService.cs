using UsersManagement.Domain.Dtos.Account;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Permissions;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<(UserAuthResponse Result, UserDto? user)> LoginUser(AuthenticateDto input);
        Task<ResponseDto<bool>> ForgetPassword(string mobile);
        Task<ResponseDto<bool>> ConfirmOtp(ConfrimOtpDto request);
        Task<ResponseDto<bool>> SubmitPassword(ForgetPasswordDto request);

        Task<bool> CheckUserHavePermission(Guid roleId, Guid permissionId);
        Task<ResponseDto<IEnumerable<UsersListDto>>> GetPaginatedUsersList(PaginationDto request);
        Task<ResponseDto<bool>> AddUser(AddUserDto requset, Guid operatorId);
        Task<ResponseDto<bool>> DeleteUserByUserId(Guid userId, Guid operatorId);
        Task<ResponseDto<bool>> ToggleActiveStatusByUserId(Guid userId, Guid operatorId);
        Task<ResponseDto<UserDetailDto>> GetUserDetailByUserId(Guid userId);
        Task<ResponseDto<bool>> UpdateUser(Guid userId, UpdateUserDto request, Guid operatorId);
        Task<ResponseDto<bool>> ChangePassword(Guid userId, string password);
    }
}
