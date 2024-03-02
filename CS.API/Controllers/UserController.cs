using CS.BL.Interfaces;
using CS.BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _userSevice;

        public UserController(IUserService userSevice)
        {
            _userSevice = userSevice;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SignUp()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            return Ok();
        }
    }
}
