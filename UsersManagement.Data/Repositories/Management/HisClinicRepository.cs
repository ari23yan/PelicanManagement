using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Data.Context;
using UsersManagement.Data.Repositories.GenericRepositories;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Entities.Dataware;
using UsersManagement.Domain.Entities.HisClinic;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces.Management;

namespace UsersManagement.Data.Repositories.Management
{
    public class HisClinicRepository : HisClinicGenericRepository<ApiUsers>, IHisClinicRepository
    {
        public HisClinicRepository(HisClinicDbContext context) : base(context)
        {
        }

        public async Task<ApiUsers> Get(string userId)
        {
            return await Context.ApiUsers.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ListResponseDto<ApiUsers>> GetPaginatedUsersList(PaginationDto paginationRequest)
        {

            ListResponseDto<ApiUsers> responseDto = new ListResponseDto<ApiUsers>();

            var skipCount = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize;
            IQueryable<ApiUsers> query = Context.ApiUsers;


            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = query.Where(u => u.MedicalNo.Contains(paginationRequest.Searchkey) || u.PhoneNumber.Contains(paginationRequest.Searchkey));
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
