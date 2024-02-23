using CS.BL.Interfaces;
using CS.BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userSevice;

        public UserController(IUserService userSevice)
        {
            _userSevice = userSevice;
        }
    }
}
