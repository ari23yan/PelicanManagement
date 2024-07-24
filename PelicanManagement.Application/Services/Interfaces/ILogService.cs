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
    }
}
