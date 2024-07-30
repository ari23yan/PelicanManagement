using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CheckUserExist(string phoneNumber);
        Task<bool> CheckUserExistByPhoneMumber(string phoneNumber);
        Task<bool> CheckUserExistByUsername(string username);
        Task<bool> CheckUserExistByEmail(string email);
        Task<User> GetUserByMobile(string mobile);
        Task<User?> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User?> GetUserDetailById(Guid userId);
        Task<User> GetUserById(Guid id);
        Task<User> GetActiveORDeActiveUserById(Guid id);
        Task<IEnumerable<Role>> GetRolesList();
        Task<Role> GetRoleById(Guid roleId);

        Task<Role> GetRoleWithDetailById(Guid roleId);
        Task<IEnumerable<Permission>> GetAllPermissions();
        Task<IEnumerable<Permission>> GetRolePermissions(Guid roleId);
        Task<IEnumerable<Menu>> GetAllMenus();
        Task<IEnumerable<Menu>> GetMenusList();
        Task<ListResponseDto<User>> GetPaginatedUsersList(PaginationDto request);


        Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset);
    }
}
