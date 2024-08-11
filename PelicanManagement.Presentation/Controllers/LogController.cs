using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Presentation.Controllers.Common;

namespace PelicanManagement.Presentation.Controllers
{

    public class LogController : BaseController
    {

        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public LogController(IUserService userService, ILogService logService,
        IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        [HttpGet]
        [PermissionChecker(Permission = PermissionType.GetLogList)]
        [Route("log/list")]
        public async Task<IActionResult> List([FromQuery] PaginationDto request)
        {
            try
            {
                var result = await _logService.GetPaginatedRolesList(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                #region Inserting Log 
                if (_configuration.GetValue<bool>("ApplicationLogIsActive"))
                {

                    var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"];
                    var userIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
                    var routeData = ControllerContext.RouteData;
                    var controllerName = routeData.Values["controller"]?.ToString();
                    var actionName = routeData.Values["action"]?.ToString();
                    _logService.InsertLog(userIp, controllerName, actionName, userAgent, ex);
                }
                #endregion
                return Ok(new ResponseDto<Exception> { IsSuccessFull = false, Data = ex, Message = ErrorsMessages.InternalServerError, Status = "Internal Server Error" });
            }
        }
    }
}
