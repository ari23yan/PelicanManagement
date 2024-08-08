using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Interfaces;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Common.AccessLog;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Domain.Entities.PelicanManagement.Common;
using PelicanManagement.Domain.Dtos.Common;

namespace PelicanManagement.Application.Services.Implementations
{
    public class LogService : ILogService
    {
        private protected IRepository<ApplicationLog> _logRepository;
        private readonly IRepository<UserActivityLog> _usersAccessLogRepository;

        public LogService(IRepository<ApplicationLog> repository, IRepository<UserActivityLog> userActivityLogRepository)
        {
            _logRepository = repository;
            _usersAccessLogRepository = userActivityLogRepository;
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


     
    }
}
