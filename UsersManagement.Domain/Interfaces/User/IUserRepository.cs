using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Dtos.UserActivity;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Entities.UsersManagement.Common;
using UsersManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Domain.Interfaces
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
        Task<List<UserActivityLog>> GetActivitiesLogByUserId(Guid userId);
        Task<User?> GetUserDetailById(Guid userId);
        Task<User> GetUserById(Guid id);
        Task<User> GetActiveORDeActiveUserById(Guid id);
        Task<bool> CheckUserHavePermission(Guid roleId, Guid permissionId);
        Task<ListResponseDto<User>> GetPaginatedUsersList(PaginationDto request);
        Task<ListResponseDto<UserActivityDto>> GetPaginatedUserActivityList(PaginationDto request);

    }
}
