using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Domain.Interfaces;
using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersManagement.Application.Utilities;
using UsersManagement.Domain.Dtos.Common.AccessLog;
using UsersManagement.Domain.Enums;
using UsersManagement.Domain.Entities.UsersManagement.Common;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.UserActivity;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.User;

namespace UsersManagement.Application.Services.Implementations
{
    public class LogService : ILogService
    {
        private protected IRepository<ApplicationLog> _logRepository;
        private readonly IRepository<UserActivityLog> _usersAccessLogRepository;
        private readonly IUserRepository _userRepository;


        public LogService(IRepository<ApplicationLog> repository, IUserRepository userRepository, IRepository<UserActivityLog> userActivityLogRepository)
        {
            _logRepository = repository;
            _usersAccessLogRepository = userActivityLogRepository;
            _userRepository = userRepository;
        }

        public async void InsertLog(string ip, string controllerName, string actionName, string userAgent, Exception ex)
        {
            ApplicationLog log = new ApplicationLog
            {
                ActionName = actionName,
                ControllerName = controllerName,
                Exception = ex == null ? null : ex.ToString(),
                IpAddress = ip,
                Message = ex.Message == null ? null : ex.Message,
                Source = ex.Source == null ? null : ex.Source,
                InnerException = ex.InnerException == null ? null : ex.InnerException.ToString(),
                Timestamp = DateTime.Now,
                UserAgent = userAgent,
            };
            await _logRepository.AddAsync(log);
        }

        public async Task<bool> InsertUserActivityLog(UserActivityLogDto userActivityLogDto)
        {
            UserActivityLog log = new UserActivityLog
            {
                Description = UtilityManager.GetActivityLogDescription(userActivityLogDto),
                Timestamp = DateTime.Now,
                UserActivityLogTypeId = ((long)userActivityLogDto.UserActivityLogTypeId),
                UserId = userActivityLogDto.UserId,
                NewValues = userActivityLogDto.NewValues == null ? null : userActivityLogDto.NewValues,
                OldValues = userActivityLogDto.OldValues == null ? null : userActivityLogDto.OldValues
            };
            await _usersAccessLogRepository.AddAsync(log);
            return true;
        }

        public async Task<ResponseDto<IEnumerable<UserActivityDto>>> GetPaginatedRolesList(PaginationDto request)
        {
            var activities = await _userRepository.GetPaginatedUserActivityList(request);
            return new ResponseDto<IEnumerable<UserActivityDto>>
            {
                IsSuccessFull = true,
                Data = activities.List,
                Message = ErrorsMessages.OperationSuccessful,
                Status = "SuccessFul",
                TotalCount = activities.TotalCount
            };
        }
    }
}
