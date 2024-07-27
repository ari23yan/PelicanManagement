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
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Application.Security;
using System.Reflection;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Dtos.Common.Pagination;

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
        public async Task<Role?> GetRoleWithDetailById(Guid roleId)
        {
            return await Context.Roles
            .Include(u => u.RoleMenus)
            .Include(u => u.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(u => u.Id.Equals(roleId) && !u.IsDeleted && u.IsActive);
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

        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            return await Context.Permissions.Where(u => !u.IsDeleted && u.IsActive)
           .ToListAsync();
        }
        public async Task<IEnumerable<Menu>> GetMenusList()
        {
            return await Context.Menus.Where(x => !x.IsDeleted && x.IsActive).OrderBy(x => x.CreatedDate).ToListAsync();
        }
        public async Task<IEnumerable<Role>> GetRolesList()
        {
            return await Context.Roles.Where(x => !x.IsDeleted && x.IsActive).ToListAsync();
        }

        public async Task<Role?> GetRoleById(Guid roleId)
        {
            return await Context.Roles.FirstOrDefaultAsync(u => u.Id.Equals(roleId) && !u.IsDeleted && u.IsActive);
        }


        public async Task<IEnumerable<Menu>> GetAllMenus()
        {
            return await Context.Menus
            .Where(u => !u.IsDeleted && u.IsActive)
            .ToListAsync();
        }

        public async Task<User?> GetUserByMobile(string mobile)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(mobile));
        }

        public async Task<bool> CheckUserExistByPhoneMumber(string phoneNumber)
        {
            return await Context.Users.AnyAsync(x => x.PhoneNumber.Equals(phoneNumber) && !x.IsDeleted && x.IsActive);
        }

        public async Task<bool> CheckUserExistByUsername(string username)
        {
            return await Context.Users.AnyAsync(x => x.Username.Equals(username) && !x.IsDeleted && x.IsActive);
        }

        public async Task<bool> CheckUserExistByEmail(string email)
        {
            return await Context.Users.AnyAsync(x => x.Email.Equals(email) && !x.IsDeleted && x.IsActive);
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Username.Equals(username));
        }
        public async Task<ListResponseDto<User>> GetPaginatedUsersList(PaginationDto paginationRequest)
        {
            ListResponseDto<User> responseDto = new ListResponseDto<User>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            IQueryable<User> query = Context.Users.Include(x => x.Role).Where(u => !u.IsDeleted && u.IsActive);


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

        public async Task<ResponseDto<RegisterUserDto>> RegisterUser(RegisterUserDto requset)
        {
            if (await CheckUserExist(requset.PhoneNumber))
            {
                return new ResponseDto<RegisterUserDto>
                { IsSuccessFull = false, Message = ErrorsMessages.UserExist };
            }
            PelicanManagement.Domain.Entities.Account.User user = new Domain.Entities.Account.User
            {
                PhoneNumber = requset.PhoneNumber,
                Email = requset.Email,
                Username = requset.PhoneNumber,
                //UserRoleId = 2,
                Password = _passwordHasher.EncodePasswordMd5(requset.Password),
                FirstName = requset.FirstName,
                LastName = requset.LastName,
            };
            await AddAsync(user);
            return new ResponseDto<RegisterUserDto>
            { IsSuccessFull = true, Message = ErrorsMessages.SuccessRegister };
        }
    }
}
