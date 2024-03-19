using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSevice;

        public UserController(IUserService userSevice)
        {
            _userSevice = userSevice;
        }

        [Authorize]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            var userDto = new UserSignUpDto()
            {
                Id = userId,
                Email = userEmail,
                RoleName = userRole,
            };

            var user = await _userSevice.Create(userDto);

            return Ok(user);
        }
    }
}
