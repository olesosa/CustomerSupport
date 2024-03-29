using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CS.DOM.Helpers;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userSevice;

        private const string ngrok = "";

        public UsersController(IUserService userSevice)
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
            var userName = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var userRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            var userDto = new UserSignUpDto()
            {
                Id = userId,
                Name = userName,
                Email = userEmail,
                RoleName = userRole,
            };

            var user = await _userSevice.Create(userDto);

            return Ok(user);
        }

        [Authorize(Policy = "User")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var auth = Request.Headers.Authorization;

            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", auth.ToString());

            var response = await client.DeleteAsync($"{ngrok}/api/Users");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(500, "Can not delete user");
            }

            await _userSevice.Delete(userId);
            
            return Ok("User was deleted");
        }
    }
}