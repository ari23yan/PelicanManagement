using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PelicanManagement.Application.Services.Implementations;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.Management.IdentityServer;
using PelicanManagement.Domain.Dtos.Role;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Presentation.Controllers.Common;

namespace PelicanManagement.Presentation.Controllers
{

    public class MangementController : BaseController
    {
        private readonly IManagementService _managementService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;
        public MangementController(ILogService logService,
        IHttpContextAccessor httpContextAccessor, IManagementService managementService, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
            _managementService = managementService;
        }
        #region Identity4Server


        [HttpGet]
        //[PermissionChecker(Permission = PermissionType.GetUserList)]
        [Route("management/identity/list")]
        public async Task<IActionResult> List([FromQuery] PaginationDto request)
        {
            try
            {
                var result = await _managementService.GetPaginatedUsersList(request);
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

        [HttpPost]
        [Route("management/identity/get")]
        //[PermissionChecker(Permission = PermissionType.GetRole)]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                var result = await _managementService.GetUserByUserId(userId);
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

        [HttpPost]
        //[PermissionChecker(Permission = PermissionType.AddRole)]
        [Route("management/identity/add")]
        public async Task<IActionResult> Add([FromBody] AddIdentityUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.AddUser(request, currentUser);
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


        [HttpDelete]
        //[PermissionChecker(Permission = PermissionType.DeleteRole)]
        [Route("management/identity/delete")]
        public async Task<IActionResult> Delete(int userId)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.DeleteUser(userId, currentUser);
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


        [HttpPut]
        //[PermissionChecker(Permission = PermissionType.UpdateRole)]
        [Route("management/identity/update")]
        public async Task<IActionResult> Update([FromQuery] int userId, [FromBody] UpdateIdentityUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.UpdateUser(userId, request, currentUser);
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



        #endregion


        #region Pelican

        [HttpGet]
        //[PermissionChecker(Permission = PermissionType.GetUserList)]
        [Route("management/pelican/list")]
        public async Task<IActionResult> PelicanList([FromQuery] PaginationDto request)
        {
            try
            {
                var result = await _managementService.GetPelicanPaginatedUsersList(request);
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



        [HttpPost]
        [Route("management/pelican/get")]
        //[PermissionChecker(Permission = PermissionType.GetRole)]
        public async Task<IActionResult> PelicaGet(string userId)
        {
            try
            {
                var result = await _managementService.GetUserByUserId(userId);
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
        #endregion

    }
}
