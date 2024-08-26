using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersManagement.Application.Services.Implementations;
using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Application.Utilities;
using UsersManagement.Domain.Dtos.Account;
using UsersManagement.Domain.Dtos.Common;
using UsersManagement.Domain.Dtos.Common.Pagination;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.Management.Dataware;
using UsersManagement.Domain.Dtos.Management.HisClinic;
using UsersManagement.Domain.Dtos.Management.IdentityServer;
using UsersManagement.Domain.Dtos.Role;
using UsersManagement.Domain.Enums;
using UsersManagement.Presentation.Controllers.Common;

namespace UsersManagement.Presentation.Controllers
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


        [HttpGet]
        [PermissionChecker(Permission = PermissionType.GetPelicanUsersList)]
        [Route("management/list")]
        public async Task<IActionResult> List([FromQuery] PaginationDto request, UserType type)
        {
            try
            {
                if (type == UserType.Pelican || type == UserType.Triage)
                {
                    var result = await _managementService.GetPaginatedUsersList(request, type);
                    return Ok(result);

                }
                else if(type == UserType.Clinic)
                {
                    var result = await _managementService.GetClinicPaginatedUsersList(request);
                    return Ok(result);
                }
                else
                {
                    var result = await _managementService.GetHisNovinPaginatedUsersList(request);
                    return Ok(result);
                }
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
        [Route("management/get")]
        [PermissionChecker(Permission = PermissionType.GetPelicanUser)]
        public async Task<IActionResult> Get([FromBody] GetUserRequest request)
        {
            try
            {
                var result = await _managementService.GetUserDetailByUserId(request.Username);
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
        [Route("management/get-clinic-user")]
        [PermissionChecker(Permission = PermissionType.GetClinicUsersDetail)]
        public async Task<IActionResult> GetClinicUser([FromBody] GetUserRequest request)
        {
            try
            {
                var result = await _managementService.GetClinicUserDetailByUserId(request.Username);
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
        [Route("management/get-his-novin-user")]
        [PermissionChecker(Permission = PermissionType.GetHisNovinUsersDetail)]
        public async Task<IActionResult> GetHisNovinUser([FromBody] GetUserRequest request)
        {
            try
            {
                var result = await _managementService.GetHisNovinUserDetailByUserId(request.Username);
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
        [Route("management/get-teriage-user")]
        [PermissionChecker(Permission = PermissionType.GetPelicanUser)]
        public async Task<IActionResult> GetTeriageUser([FromBody] GetUserRequest request)
        {
            try
            {
                var result = await _managementService.GetTeriageUserDetailByUserId(request.Username);
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
        [PermissionChecker(Permission = PermissionType.AddPelicanUser)]
        [Route("management/add")]
        public async Task<IActionResult> Add([FromBody] AddIdentityUserDto request, UserType type)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.AddUser(request, currentUser,type);
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
        [PermissionChecker(Permission = PermissionType.AddClinicUsers)]
        [Route("management/add-clinic-user")]
        public async Task<IActionResult> AddClinicUser([FromBody] AddClinicUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.AddClinicUser(request, currentUser);

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
        [PermissionChecker(Permission = PermissionType.AddClinicUsers)]
        [Route("management/add-his-novin-user")]
        public async Task<IActionResult> AddHisnovinUser([FromBody] AddHisNovinUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.AddHisNovinUser(request, currentUser);
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
        [PermissionChecker(Permission = PermissionType.DeletePelicanUser)]
        [Route("management/delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserRequest request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.DeleteUser(request.UserId, currentUser);
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
        [PermissionChecker(Permission = PermissionType.DeleteClinicUsers)]
        [Route("management/delete-clinic-user")]
        public async Task<IActionResult> DeleteClinicUser([FromBody] GetUserRequest request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.DeleteClinicUser(request.Username, currentUser);
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
        [PermissionChecker(Permission = PermissionType.DeleteHisNovinUsers)]
        [Route("management/delete-his-novin-user")]
        public async Task<IActionResult> DeleteHisNovinUser([FromBody] GetUserRequest request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.DeleteHisnovinUser(request.Username, currentUser);
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
        [PermissionChecker(Permission = PermissionType.UpdateClinicUsers)]
        [Route("management/update-clinic-user")]
        public async Task<IActionResult> UpdateClinicUser([FromQuery] string userId, [FromBody] UpdateClinicUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.UpdateClinicUser(userId, request, currentUser);
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
        [PermissionChecker(Permission = PermissionType.UpdateHisNovinUsers)]
        [Route("management/update-his-novin-user")]
        public async Task<IActionResult> UpdateHisNovinUser([FromQuery] string userId, [FromBody] UpdateHisNovinUserDto request)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _managementService.UpdateHisNovinUser(userId, request, currentUser);
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
        [PermissionChecker(Permission = PermissionType.UpdatePelicanUser)]
        [Route("management/update")]
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



        [HttpGet]
        [Route("management/get-permissons-and-units-list")]  // For ComboList
        public async Task<IActionResult> GetPermissionsAndUnits()
        {
            try
            {
                var result = await _managementService.GetPermissionsAndUnits();
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
