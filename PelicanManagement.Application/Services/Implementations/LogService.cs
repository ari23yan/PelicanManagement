using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Interfaces;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Domain.Entities.Common;
using PelicanManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelicanManagement.Application.Services.Implementations
{
    public class LogService : ILogService
    {
        private protected IRepository<ApplicationLog> _logRepository;
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
                Exception = ex.ToString(),
                IpAddress = ip,
                Message = ex.Message,
                Source = ex.Source,
                InnerException = ex.InnerException.ToString(),
                Timestamp = DateTime.Now,
                UserAgent = userAgent,
            };
            await _logRepository.AddAsync(log);
        }
    }
}
