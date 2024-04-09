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
        private static readonly string ApiIdentityAddress = ConstVariables.ApiIdentityAddress;

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

            var userDto = new UserSignUpDto()
            {
                Id = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                Name = HttpContext.User.FindFirstValue(ClaimTypes.Name),
                Email = HttpContext.User.FindFirstValue(ClaimTypes.Email),
                RoleName = HttpContext.User.FindFirstValue(ClaimTypes.Role),
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

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", auth.ToString());

            var response = await client.DeleteAsync($"{ApiIdentityAddress}/Users");

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(500, "Can not delete user");
            }

            await _userSevice.Delete(userId);
            
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

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var user = await _userSevice.GetById(userId);

            return Ok(user);
        }
    }
}