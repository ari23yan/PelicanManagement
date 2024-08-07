using Microsoft.EntityFrameworkCore;
using PelicanManagement.Data.Context;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Entities.IdentityServer;
using PelicanManagement.Domain.Entities.PelicanManagement.Account;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces;
using PelicanManagement.Domain.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Data.Repositories.Management
{
    public class IdentityServerRepository : IdentityServerGenericRepository<Domain.Entities.IdentityServer.User>, IIdentityServerRepository
    {
        public IdentityServerRepository(IdentityServerDbContext context) : base(context)
        {
        }

        public async Task<Domain.Entities.IdentityServer.User> Get(int userId)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<Domain.Entities.IdentityServer.User> GetByUsername(string username)
        {
            return await Context.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<ListResponseDto<Domain.Entities.IdentityServer.User>> GetPaginatedUsersList(PaginationDto paginationRequest)
        {
            ListResponseDto<Domain.Entities.IdentityServer.User> responseDto = new ListResponseDto<Domain.Entities.IdentityServer.User>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            IQueryable<Domain.Entities.IdentityServer.User> query = Context.Users;

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
