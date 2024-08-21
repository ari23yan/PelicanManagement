using Microsoft.EntityFrameworkCore;
using UsersManagement.Data.Context;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Security;
using System.Reflection;
using UsersManagement.Domain.Entities.UsersManagement.Common;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.UserActivity;

namespace UsersManagement.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(IPasswordHasher passwordHasher, AppDbContext context) : base(context)
        {
            _passwordHasher = passwordHasher;
        }
        public async Task<bool> CheckUserExist(string phoneNumber)
        {
            return await Context.Users
                 .AnyAsync(x => x.PhoneNumber.Equals(phoneNumber) && !x.IsDeleted && x.IsActive);
        }
        public async Task<User?> GetUserByEmail(string phoneNumber)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(phoneNumber) && !x.IsDeleted && x.IsActive);
        }
        public async Task<User?> GetUserById(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted && x.IsActive);
        }
        public async Task<User?> GetActiveORDeActiveUserById(Guid id)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.IsDeleted);
        }
        public async Task<User?> GetUserDetailById(Guid userId)
        {
            return await Context.Users
                .Include(u => u.Role)
                .ThenInclude(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .Include(u => u.Role).ThenInclude(x=>x.RoleMenus)
                .IgnoreAutoIncludes()
                .FirstOrDefaultAsync(u => u.Id.Equals(userId) && !u.IsDeleted && u.IsActive);
        }
        public async Task<bool> CheckUserHavePermission(Guid roleId, Guid permissionId)
        {
            return await Context.RolePermissions.AnyAsync(x => x.RoleId.Equals(roleId) && x.PermissionId.Equals(permissionId));
        }
        public async Task<User?> GetUserByMobile(string mobile)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(mobile));
        }
        public async Task<bool> CheckUserExistByPhoneMumber(string phoneNumber)
        {
            return await Context.Users.AnyAsync(x => x.PhoneNumber.Equals(phoneNumber) && !x.IsDeleted);
        }
        public async Task<bool> CheckUserExistByUsername(string username)
        {
            return await Context.Users.AnyAsync(x => x.Username.Equals(username) && !x.IsDeleted);
        }
        public async Task<bool> CheckUserExistByEmail(string email)
        {
            return await Context.Users.AnyAsync(x => x.Email.Equals(email) && !x.IsDeleted);
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Username.Equals(username));
        }
        public async Task<ListResponseDto<User>> GetPaginatedUsersList(PaginationDto paginationRequest)
        {
            ListResponseDto<User> responseDto = new ListResponseDto<User>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            IQueryable<User> query = Context.Users.Include(x => x.Role).Where(u => !u.IsDeleted);


            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = query.Where(u => u.LastName.Contains(paginationRequest.Searchkey));
            }

            query = paginationRequest.FilterType == FilterType.Asc ?
                query.OrderBy(u => u.Id) :
                query.OrderByDescending(u => u.Id);


            responseDto.TotalCount = await query.CountAsync();
            var pagedQuery = query.Skip(skipCount).Take(paginationRequest.PageSize);
            responseDto.List = await pagedQuery.ToListAsync();
            return responseDto;
        }

        public async Task<List<UserActivityLog>> GetActivitiesLogByUserId(Guid userId)
        {
            return await Context.UserActivityLogs.Where(x => x.UserId == userId).Take(10).ToListAsync();
        }

        public async Task<ListResponseDto<UserActivityDto>> GetPaginatedUserActivityList(PaginationDto paginationRequest)
        {
            ListResponseDto<UserActivityDto> responseDto = new ListResponseDto<UserActivityDto>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;

                IQueryable<UserActivityDto> query = (from activity in Context.UserActivityLogs
                              join user in Context.Users on activity.UserId equals user.Id
                              select new UserActivityDto
                              {
                                  NewValues = activity.NewValues,
                                  OldValues = activity.OldValues,
                                  Description = activity.Description,
                                  Timestamp = activity.Timestamp,
                                  UserActivityLogTypeId = activity.UserActivityLogTypeId,
                                  UserId = activity.UserId,
                                  Username = user.Username,
                              }
                              ).Distinct();
            ;

            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = query.Where(u => u.NewValues.Contains(paginationRequest.Searchkey) || u.Username.Contains(paginationRequest.Searchkey) || u.OldValues.Contains(paginationRequest.Searchkey));
            }

            query = paginationRequest.FilterType == FilterType.Asc ?
                query.OrderBy(u => u.Timestamp) :
                query.OrderByDescending(u => u.Timestamp);

            responseDto.TotalCount = await query.CountAsync();
            var pagedQuery = query.Skip(skipCount).Take(paginationRequest.PageSize);
            responseDto.List = await pagedQuery.ToListAsync();
            return responseDto;
        }
    }
}
