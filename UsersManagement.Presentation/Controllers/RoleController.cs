using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.Services.Implementations;
using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Application.Utilities;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Enums;
using UsersManagement.Presentation.Controllers.Common;

namespace UsersManagement.Presentation.Controllers
{
    public class RoleController : BaseController
    {

        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public RoleController(ILogService logService,
        IHttpContextAccessor httpContextAccessor, IRoleService roleService, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
            _roleService = roleService;
        }

        [HttpGet]
        [PermissionChecker(Permission = PermissionType.GetRoleList)]
        [Route("role/list")]
        public async Task<IActionResult> List([FromQuery] PaginationDto request)
        {
            try
            {
                var result = await _roleService.GetPaginatedRolesList(request);
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
        [Route("role/get")]
        [PermissionChecker(Permission = PermissionType.GetRole)]
        public async Task<IActionResult> Get(GetByIdDto request)
        {
            try
            {
                var result = await _roleService.GetRoleMenusByRoleId(request.TargetId.Value);
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
        [PermissionChecker(Permission = PermissionType.AddRole)]
        [Route("role/add")]
        public async Task<IActionResult> Add([FromBody] AddRoleDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _roleService.AddRole(request, currentUser);
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
        [PermissionChecker(Permission = PermissionType.DeleteRole)]
        [Route("role/delete")]
        public async Task<IActionResult> Delete(GetByIdDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _roleService.DeleteRoleByRoleId(request.TargetId.Value, currentUser);
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
        [PermissionChecker(Permission = PermissionType.ActiveStatusRole)]
        [Route("role/toggle-active-status")]

        public async Task<IActionResult> ToggleActiveStatus(GetByIdDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _roleService.ToggleActiveStatusByRoleId(request.TargetId.Value, currentUser);
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
        [PermissionChecker(Permission = PermissionType.UpdateRole)]
        [Route("role/update")]
        public async Task<IActionResult> Update([FromQuery] Guid roleId, [FromBody] UpdateRoleDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _roleService.UpdateRole(roleId, request, currentUser);

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


        [HttpGet]
        [Route("role/get-list")]  // For ComboList
        public async Task<IActionResult> GetRolesList()
        {
            try
            {
                var result = await _roleService.GetRolesList();
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
        [Route("role/get-role-permissions")]
        public async Task<IActionResult> GetRolePermissions(GetByIdDto request)
        {
            try
            {
                var result = await _roleService.GetRolePermissionsByRoleId(request);

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
        [Route("role/get-role-menu")]
        public async Task<IActionResult> GetRoleMenu(GetByIdDto request)
        {
            try
            {
                var result = await _roleService.GetRoleMenusByRoleId(request.TargetId.Value);
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

        [HttpGet]
        [Route("role/get-permissons-and-menus-list")]  // For ComboList
        public async Task<IActionResult> GetRolePermissionsAndMenus([FromQuery] GetByIdDto request)
        {
            try
            {
                var result = await _roleService.GetRolesPermissionAndMenus(request.TargetId);
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
