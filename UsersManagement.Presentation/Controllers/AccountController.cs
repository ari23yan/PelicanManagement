using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using UsersManagement.Application.Security;
using UsersManagement.Application.Services.Interfaces;
using UsersManagement.Application.Utilities;
using UsersManagement.Domain.Dtos.Account;
using UsersManagement.Domain.Dtos.Common.ResponseModel;
using UsersManagement.Domain.Dtos.User;
using UsersManagement.Domain.Enums;
using UsersManagement.Presentation.Controllers.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UsersManagement.Presentation.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public AccountController(IUserService userService, ILogService logService,
        IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logService = logService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateDto request)
        {
            try
            {
                var result = await _userService.LoginUser(request);
                switch (result.Result)
                {
                    case UserAuthResponse.WrongPassword:
                        return Ok(new ResponseDto<UserDto> { IsSuccessFull = false, Message = ErrorsMessages.InvalidUsernameOrPassword });
                    case UserAuthResponse.NotAvtive:
                        return Ok(new ResponseDto<UserDto> { IsSuccessFull = false, Message = ErrorsMessages.AccountInactive });
                    case UserAuthResponse.NotFound:
                        return Ok(new ResponseDto<UserDto> { IsSuccessFull = false, Message = ErrorsMessages.NotFound });
                    case UserAuthResponse.EmailNotConfirmed:
                        return Ok(new ResponseDto<UserDto> { IsSuccessFull = false, Message = ErrorsMessages.UserNotFound });
                    case UserAuthResponse.Success:

                        var claims = new List<Claim>
                        {
                         new Claim(ClaimTypes.NameIdentifier,result.user.Id.ToString()),
                         new Claim(ClaimTypes.Role,result.user.RoleId.ToString())
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:IssuerSigningKey"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: _configuration["Authentication:Issuer"],
                            audience: _configuration["Authentication:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddDays(2),
                            signingCredentials: credentials
                   );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                        result.user.Token = tokenString;
                        return Ok(new ResponseDto<UserDto> { IsSuccessFull = true, Data = result.user, Message = ErrorsMessages.SuccessfulLogin });
                    default:
                        return BadRequest(new ResponseDto<UserDto> { IsSuccessFull = false, Message = ErrorsMessages.FailedLogin });
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


        [AllowAnonymous]
        [HttpPost]
        [Route("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordPhoneNumberDto request)
        {
            try
            {
                var result = await _userService.ForgetPassword(request.PhoneNumber);
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


        [AllowAnonymous]
        [HttpPost]
        [Route("confirm-otp")]
        public async Task<IActionResult> ConfirmOtp(ConfrimOtpDto request)
        {
            try
            {
                var result = await _userService.ConfirmOtp(request);
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

        [AllowAnonymous]
        [HttpPut]
        [Route("forget-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto request)
        {
            try
            {
                var result = await _userService.SubmitPassword(request);
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
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] string password)
        {
            try
            {
                var currentUser = UtilityManager.GetCurrentUser(_httpContextAccessor);
                var result = await _userService.ChangePassword(currentUser, password);
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
