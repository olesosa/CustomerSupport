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
        private readonly IUserService _userService;
        private static readonly string ApiIdentityAddress = Environments.ApiIdentityAddress;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userDto = new UserInfoDto()
            {
                Id = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = HttpContext.User.FindFirstValue(ClaimTypes.Name),
                Email = HttpContext.User.FindFirstValue(ClaimTypes.Email),
                RoleName = HttpContext.User.FindFirstValue(ClaimTypes.Role)
            };

            var user = await _userService.Create(userDto);

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

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", auth.ToString());

            var response = await client.DeleteAsync($"{ApiIdentityAddress}/Users");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(500, "Can not delete user");
            }

            await _userService.Delete(userId);
            
            return Ok("User was deleted");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new UserInfoDto()
            {
                Id = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = HttpContext.User.FindFirstValue(ClaimTypes.Name),
                Email = HttpContext.User.FindFirstValue(ClaimTypes.Email),
                RoleName = HttpContext.User.FindFirstValue(ClaimTypes.Role)
            };

            if (!await _userService.IsUserExist(user.Id))
            {
                await _userService.Create(user);
            }

            return Ok(user);
        }
    }
}