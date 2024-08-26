using Microsoft.Data.SqlClient;
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
    public class DatawareRepository : DatawareGenericRepository<AspNetUser>, IDatawareRepository
    {
        public DatawareRepository(DatawareDbContext context) : base(context)
        {
        }

        public async Task<AspNetUser> Get(string userId)
        {
            return await Context.AspNetUsers.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<ListResponseDto<AspNetUser>> GetPaginatedUsersList(PaginationDto paginationRequest)
        {
            ListResponseDto<AspNetUser> responseDto = new ListResponseDto<AspNetUser>();

            var query = Context.AspNetUsers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(paginationRequest.Searchkey))
            {
                query = query.Where(u => u.UserName.Contains(paginationRequest.Searchkey)
                                         || u.LastName.Contains(paginationRequest.Searchkey));
            }

            responseDto.TotalCount = await query.CountAsync();

            var startRow = (paginationRequest.PageNumber - 1) * paginationRequest.PageSize + 1;
            var endRow = paginationRequest.PageNumber * paginationRequest.PageSize;

            var paginatedQuery = await Context.AspNetUsers
                .FromSqlRaw(@"
            SELECT * FROM 
            (
                SELECT *, ROW_NUMBER() OVER (ORDER BY UserName) AS RowNum
                FROM AspNetUsers
                WHERE (@Searchkey IS NULL OR UserName LIKE '%' + @Searchkey + '%' OR LastName LIKE '%' + @Searchkey + '%')
            ) AS RowConstrainedResult
            WHERE RowNum >= @StartRow AND RowNum <= @EndRow
            ORDER BY RowNum
            ",
                    new SqlParameter("@Searchkey", (object)paginationRequest.Searchkey ?? DBNull.Value),
                    new SqlParameter("@StartRow", startRow),
                    new SqlParameter("@EndRow", endRow))
                .ToListAsync();
            responseDto.List = paginatedQuery;
            return responseDto;
        }


    }
}
