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

            try
            {
                Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
                var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

                var userDto = new UserSignUpDto()
                {
                    Id = userId,
                    Email = userEmail,
                    RoleName = userRole,
                };

                if (!await _userSevice.DoEmailExist(userEmail))
                {
                    return BadRequest("User already exist");
                }
                if (await _userSevice.Create(userDto))
                {
                    return Ok("User was created");
                }
                else
                {
                    return BadRequest("Somth went wrong");
                }
            }
            catch { }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
