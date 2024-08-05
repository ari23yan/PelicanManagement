using Microsoft.EntityFrameworkCore;
using PelicanManagement.Data.Context;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Entities.Pelican;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Interfaces.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Data.Repositories.Management
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
    }
}
