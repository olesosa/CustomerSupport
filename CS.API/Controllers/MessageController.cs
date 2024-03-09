using CS.BL.Interfaces;
using CS.BL.Services;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Messages/{dialogId:Guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid dialogId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);

                var messages = _messageService.GetAllByDialogId(dialogId, cancellationToken);

                if(messages == null)
                {
                    return NotFound("No messages in dialog yet");
                }

                return Ok(messages);
            }
            catch { }

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageDto message)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            if(await _messageService.SendMessage(message))
            {
                return Ok("Message has been sent");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
