﻿using PelicanManagement.Domain.Dtos.Common;
using PelicanManagement.Domain.Dtos.Common.AccessLog;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Dtos.UserActivity;
using PelicanManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Interfaces
{
    public interface ILogService
    {
        void InsertLog(string ip, string controllerName, string actionName, string userAgent, Exception ex);


        Task<bool> InsertUserActivityLog(UserActivityLogDto userActivityLogDto);
        Task<ResponseDto<IEnumerable<UserActivityDto>>> GetPaginatedRolesList(PaginationDto request);
    }
}
