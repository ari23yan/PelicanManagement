using Microsoft.EntityFrameworkCore;
using UsersManagement.Data.Context;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Entities.UsersManagement.Account;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Data.Repositories.GenericRepositories;

namespace UsersManagement.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Role?> GetRoleWithDetailById(Guid roleId)
        {
            return await Context.Roles
            .Include(u => u.RoleMenus.Where(u => !u.IsDeleted && u.IsActive))
            .Include(u => u.RolePermissions.Where(u => !u.IsDeleted && u.IsActive))
            .ThenInclude(rp => rp.Permission)
            .IgnoreAutoIncludes()
            .FirstOrDefaultAsync(u => u.Id.Equals(roleId) && !u.IsDeleted && u.IsActive);
        }
        public async Task<IEnumerable<Permission>> GetRolePermissions(Guid roleId)
        {
            return await Context.RolePermissions
             .Include(x => x.Permission)
             .Where(u => u.RoleId.Equals(roleId) && !u.IsDeleted && u.IsActive)
             .Select(x => x.Permission)
             .ToListAsync();
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
        public async Task<IEnumerable<Menu>> GetMenusWithoutSubsList()
        {
            return await Context.Menus.Where(x => !x.IsDeleted && x.IsActive && x.SubMenuId == null).OrderBy(x => x.CreatedDate).ToListAsync();
        }


        public async Task<IEnumerable<Role>> GetRolesList()
        {
            return await Context.Roles.Where(x => !x.IsDeleted && x.IsActive).ToListAsync();
        }
        public async Task<Role?> GetRoleById(Guid roleId)
        {
            return await Context.Roles.FirstOrDefaultAsync(u => u.Id.Equals(roleId) && !u.IsDeleted && u.IsActive);
        }   
        public async Task<Role?> GetActiveORDeActiveRoleById(Guid roleId)
        {
            return await Context.Roles.FirstOrDefaultAsync(u => u.Id.Equals(roleId) && !u.IsDeleted);
        }
        public async Task<IEnumerable<Menu>> GetRoleMenusByRoleId(Guid roleId)
        {
            return  await Context.RoleMenus.Include(x=>x.Menu).Where(u => u.Id.Equals(roleId) && !u.IsDeleted).Select(x=>x.Menu).ToListAsync();
        }
        
        public async Task<IEnumerable<Menu>> GetAllMenus()
        {
            return await Context.Menus
            .Where(u => !u.IsDeleted && u.IsActive)
            .ToListAsync();
        }
        public async Task<ListResponseDto<Role>> GetPaginatedRolesList(PaginationDto paginationRequest)
        {
            ListResponseDto<Role> responseDto = new ListResponseDto<Role>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            IQueryable<Role> query = Context.Roles.Where(u => !u.IsDeleted);


            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = query.Where(u => u.RoleName_Farsi.Contains(paginationRequest.Searchkey));
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
