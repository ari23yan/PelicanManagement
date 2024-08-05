using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Entities.Pelican;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Domain.Interfaces.Management
{
    public interface IPelicanRepository: IPelicanGenericRepository<ApiUser>
    {
        Task<ListResponseDto<ApiUser>> GetPaginatedUsersList(PaginationDto request);
        Task<ApiUser> Get(string userId);
    }
}
