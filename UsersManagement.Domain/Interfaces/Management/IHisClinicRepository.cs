using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Entities.Dataware;
using UsersManagement.Domain.Entities.HisClinic;
using UsersManagement.Domain.Interfaces.GenericRepositories;

namespace UsersManagement.Domain.Interfaces.Management
{
    public interface IHisClinicRepository: IDatawareGenericRepository<ApiUsers>
    {
        Task<ListResponseDto<ApiUsers>> GetPaginatedUsersList(PaginationDto request);
        Task<ApiUsers> Get(string userId);
    }
}
