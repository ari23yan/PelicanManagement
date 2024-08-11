using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Dtos.UserActivity;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Entities.PelicanManagement.Common;
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
        Task<List<UserActivityLog>> GetActivitiesLogByUserId(Guid userId);
        Task<User?> GetUserDetailById(Guid userId);
        Task<User> GetUserById(Guid id);
        Task<User> GetActiveORDeActiveUserById(Guid id);
        Task<bool> CheckUserHavePermission(Guid roleId, Guid permissionId);
        Task<ListResponseDto<User>> GetPaginatedUsersList(PaginationDto request);
        Task<ListResponseDto<UserActivityDto>> GetPaginatedUserActivityList(PaginationDto request);

    }
}
