using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Entities.Dataware;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Interfaces.GenericRepositories;

namespace UsersManagement.Domain.Interfaces.Management
{
    public interface IDatawareRepository: IDatawareGenericRepository<AspNetUser>
    {
        Task<ListResponseDto<AspNetUser>> GetPaginatedUsersList(PaginationDto request);
        Task<AspNetUser> Get(string userId);
    }
}
