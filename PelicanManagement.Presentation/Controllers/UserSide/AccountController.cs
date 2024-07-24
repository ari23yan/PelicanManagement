using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Security;
using Shop.Application.Services.Interfaces;
using Shop.Domain.Dtos.User;
using Shop.Presentation.Controllers.UserSide.Common;

namespace Shop.Presentation.Controllers.UserSide
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
        [HttpPost]
        public async Task<IActionResult> Register([FromForm]RegisterUserDto request)
        {
            try
            {
                return Ok(await _userService.RegisterUser(request));
            }
            catch (Exception ex)
            {
                if (_configuration.GetValue<bool>("ApplicationLogIsActive"))
                {
                    #region Inserting Log 
                    var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"];
                    var userIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
                    var routeData = ControllerContext.RouteData;
                    var controllerName = routeData.Values["controller"]?.ToString();
                    var actionName = routeData.Values["action"]?.ToString();
                    _logService.InsertLog(userIp, controllerName, actionName, userAgent, ex);
                    #endregion
                }
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
