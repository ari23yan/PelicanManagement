using Microsoft.EntityFrameworkCore;
using PelicanManagement.Data.Context;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PelicanManagement.Application.Security;
using System.Reflection;
using PelicanManagement.Domain.Entities.PelicanManagement.Common;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;

namespace PelicanManagement.Data.Repositories
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
    }
}
