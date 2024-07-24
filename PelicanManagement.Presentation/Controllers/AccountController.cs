using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PelicanManagement.Application.Security;
using PelicanManagement.Application.Services.Interfaces;
using PelicanManagement.Application.Utilities;
using PelicanManagement.Domain.Dtos.Account;
using PelicanManagement.Domain.Dtos.Common.ResponseModel;
using PelicanManagement.Domain.Dtos.User;
using PelicanManagement.Domain.Enums;

namespace PelicanManagement.Presentation.Controllers
{

    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogService _logService;

        public AccountController(IUserService userService, ILogService logService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userService = userService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
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
                        return Unauthorized(new ResponseDto<bool> { IsSuccessFull = false, Data = false, Message = ErrorsMessages.UserORPasswrodIsWrong });
                    case UserAuthResponse.NotAvtive:
                        return Unauthorized(new ResponseDto<string> { IsSuccessFull = false, Data =null, Message = ErrorsMessages.NotActive}); 
                    case UserAuthResponse.NotFound:
                        return Unauthorized(new ResponseDto<bool> { IsSuccessFull = false, Data = false, Message = ErrorsMessages.UserNotfound });
                    case UserAuthResponse.Success:
                        return Ok(new ResponseDto<UserDto> { IsSuccessFull = true, Data = result.user, Message = ErrorsMessages.SuccessLogin });
                    default:
                        return BadRequest(new ResponseDto<bool> { IsSuccessFull = false, Data = false, Message = ErrorsMessages.Faild});
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

    }
}
