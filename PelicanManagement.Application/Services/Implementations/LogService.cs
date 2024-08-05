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

namespace PelicanManagement.Application.Services.Implementations
{
    public class LogService : ILogService
    {
        private protected IRepository<ApplicationLog> _logRepository;
        private readonly IRepository<UserActivityLog> _usersAccessLogRepository;

        public LogService(IRepository<ApplicationLog> repository)
        {
                _logRepository = repository;    
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


       

    }
}
