using Microsoft.EntityFrameworkCore;
using UsersManagement.Data.Context;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Dtos.Management.Pelican;
using UsersManagement.Domain.Entities.Pelican;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Data.Repositories.Management
{
    public class PelicanRepository : PelicanGenericRepository<ApiUser>, IPelicanRepository
    {
        public PelicanRepository(PelicanDbContext context) : base(context)
        {
        }

        public async Task<ApiUser> Get(string userId)
        {
            return await Context.ApiUsers.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ListResponseDto<ApiUser>> GetPaginatedUsersList(PaginationDto paginationRequest)
        {
            ListResponseDto<ApiUser> responseDto = new ListResponseDto<ApiUser>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            IQueryable<ApiUser> query = Context.ApiUsers;

            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = query.Where(u => u.LastName.Contains(paginationRequest.Searchkey));
            }

            query = paginationRequest.FilterType == FilterType.Asc ?
                query.OrderBy(u => u.Id) :
                query.OrderByDescending(u => u.Id);
            responseDto.TotalCount = await query.CountAsync();
            var pagedQuery = query.Skip(skipCount).Take(paginationRequest.PageSize);
            responseDto.List = await pagedQuery
              .ToListAsync();


            return responseDto;
        }

        public async Task<List<UserPermission>> GetUserPermissions(string username)
        {
            return await Context.UserPermissions
                                         .Where(up => up.UserId.Equals(username))
                                         .ToListAsync();
        }

        public async Task<PermissionsAndUnitsDto> GetPermissionsAndUnits()
        {
            PermissionsAndUnitsDto permissionsAndUnits = new PermissionsAndUnitsDto();
            permissionsAndUnits.UserUnits = await Context.Units.ToListAsync();
            permissionsAndUnits.UserPermissions = await Context.Permissions.ToListAsync();

            return permissionsAndUnits;
        }

        public async Task<List<UsersUnit>> GetUserUnits(string username)
        {
            return await Context.UsersUnits
                                           .Where(up => up.Username.Equals(username))
                                           .ToListAsync();

        }

        public async Task<List<PelicanUserPermissionsDto>> GetUserPermissionsByUsername(string username)
        {
            var allPermissions = await Context.Permissions.ToListAsync();

            var userPermissions = await Context.UserPermissions
                                                .Where(up => up.UserId.Equals(username))
                                                .ToListAsync();

            var result = allPermissions.Select(permission => new PelicanUserPermissionsDto
            {
                Description = permission.Description,
                PermissionId = permission.Id,
                PermissionName = permission.Name,
                HasPermission = userPermissions.Any(up => up.PermissionId == permission.Id)
            }).ToList();

            return result;
        }


        public async Task<List<PelicanUserUnitsDto>> GetUserUnitsByUsername(string username)
        {
            var allUnits = await Context.Units.ToListAsync();

            var userUnits = await Context.UsersUnits
                                                .Where(up => up.Username.Equals(username))
                                                .ToListAsync();
            var result = allUnits.Select(unit => new PelicanUserUnitsDto
            {
                UnitName = unit.Name,
                unitId = unit.Id,
                HaveUnit = userUnits.Any(up => up.UnitId == unit.Id)
            }).ToList();

            return result;
        }

        public async Task<ApiUser> GetByUsername(string username)
        {
            return await Context.ApiUsers.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
