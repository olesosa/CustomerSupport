using System.Security.Claims;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HubsController : ControllerBase
{
    private readonly ISignalrService _service;

    public HubsController(ISignalrService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SendMessage(ChatMessageDto message)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new UserInfoDto()
        {
            Id = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            UserName = HttpContext.User.FindFirstValue(ClaimTypes.Name),
            Email = HttpContext.User.FindFirstValue(ClaimTypes.Email),
            RoleName = HttpContext.User.FindFirstValue(ClaimTypes.Role)
        };
        
        
        await _service.SendMessage(message, user);
        
        return Ok();
    }
}