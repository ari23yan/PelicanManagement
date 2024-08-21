using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.AccessLog;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.UserActivity;
using UsersManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsersManagement.Application.Services.Interfaces
{
    public interface ILogService
    {
        void InsertLog(string ip, string controllerName, string actionName, string userAgent, Exception ex);


        Task<bool> InsertUserActivityLog(UserActivityLogDto userActivityLogDto);
        Task<ResponseDto<IEnumerable<UserActivityDto>>> GetPaginatedRolesList(PaginationDto request);
    }
}
