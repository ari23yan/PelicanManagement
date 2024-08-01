using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common;
using PelicanManagement.Domain.Dtos.Common.Pagination;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Enums;
using PelicanManagement.Presentation.Controllers.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PelicanManagement.Presentation.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public UserController(IUserService userService, ILogService logService,
        IHttpContextAccessor httpContextAccessor, IConfiguration configuration
        )
        {
            _userService = userService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

       
        [HttpGet]
        [PermissionChecker(Permission = PermissionType.GetUserList)]
        [Route("user/list")]
        public async Task<IActionResult> List([FromQuery] PaginationDto request)
        {
            try
            {
                var result = await _userService.GetPaginatedUsersList(request);
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
        [Route("user/get")]
        [PermissionChecker(Permission = PermissionType.GetUser)]
        public async Task<IActionResult> Get(GetByIdDto request)
        {
            try
            {
                var result = await _userService.GetUserDetailByUserId(request.TargetId.Value);
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
        [PermissionChecker(Permission = PermissionType.AddUser)]
        [Route("user/add")]
        public async Task<IActionResult> Add([FromBody]AddUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _userService.AddUser(request, currentUser);
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
        [PermissionChecker(Permission = PermissionType.DeleteUser)]
        [Route("user/delete")]
        public async Task<IActionResult> Delete(GetByIdDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _userService.DeleteUserByUserId(request.TargetId.Value, currentUser);
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
        [PermissionChecker(Permission = PermissionType.ActiveStatusUser)]
        [Route("user/toggle-active-status")]

        public async Task<IActionResult> ToggleActiveStatus(GetByIdDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _userService.ToggleActiveStatusByUserId(request.TargetId.Value, currentUser);
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
        [PermissionChecker(Permission = PermissionType.UpdateUser)]
        [Route("user/update")]
        public async Task<IActionResult> Update([FromQuery]Guid userId,[FromBody] UpdateUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _userService.UpdateUser(userId, request, currentUser);
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
