using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Presentation.Controllers.AdminSide.Common
{
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class AdminBaseController : ControllerBase
    {
    }
}
