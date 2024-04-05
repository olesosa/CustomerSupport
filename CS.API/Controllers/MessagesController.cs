using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("messages/{dialogId:Guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid dialogId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var messages = await _messageService.GetAllByDialogId(dialogId, cancellationToken);

            return Ok(messages);
        }

        [Authorize(Policy = "User")]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var senderId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var createdMessage = await _messageService.SendMessage(message, senderId);

            return Ok(createdMessage);
        }

        [Authorize(Policy = "User")]
        [HttpPost("receive")]
        public async Task<IActionResult> ReceiveMessage([FromBody] SendMessageDto message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            throw new NotImplementedException();
        }


    }
}
