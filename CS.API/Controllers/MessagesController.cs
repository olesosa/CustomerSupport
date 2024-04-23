using System.Security.Claims;
using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly ISignalrService _signalrService;
        public MessagesController(IMessageService messageService, ISignalrService signalrService)
        {
            _messageService = messageService;
            _signalrService = signalrService;
        }

        [Authorize]
        [HttpPost("{dialogId:guid}")]
        public async Task<IActionResult> SendMessage([FromForm] PostMessage message, [FromRoute] Guid dialogId)
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
        
            var sentMessage = await _signalrService.SendMessage(message, dialogId, user);
        
            return Ok(sentMessage);
        }
        
        [Authorize]
        [HttpPatch("{dialogId:Guid}")]
        public async Task<IActionResult> ReadMessages(Guid dialogId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _messageService.MarkAsRead(dialogId);
            
            return Ok();
        }
    }
}
