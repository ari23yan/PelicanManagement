using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Entities.IdentityServer;
using UsersManagement.Domain.Entities.Pelican;
using UsersManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Services.Interfaces
{
    public interface IManagementService
    {
        Task<ResponseDto<IEnumerable<User>>> GetPaginatedUsersList(PaginationDto request, UserType type);
        Task<ResponseDto<IEnumerable<ApiUser>>> GetPelicanPaginatedUsersList(PaginationDto request);



        Task<ResponseDto<IdentityUserDetailDto>> GetUserDetailByUserId(string userId);



        Task<ResponseDto<UsersManagement.Domain.Entities.IdentityServer.User>> GetTeriageUserDetailByUserId(string userId);


        Task<ResponseDto<PermissionsAndUnitsDto>> GetPermissionsAndUnits();





        Task<ResponseDto<bool>> AddUser(AddIdentityUserDto requset, Guid operatorId, UserType type);
        Task<ResponseDto<bool>> DeleteUser(int userID, Guid operatorId);

        Task<ResponseDto<bool>> UpdateUser(int userID, UpdateIdentityUserDto request, Guid operatorId);











    }
}
