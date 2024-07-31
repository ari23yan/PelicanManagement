using Microsoft.EntityFrameworkCore;
using PelicanManagement.Data.Context;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Entities.Account;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
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
