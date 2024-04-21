using CS.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
        [HttpGet("{dialogId:Guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid dialogId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messages = await _messageService.GetAll(dialogId, cancellationToken);

            return Ok(messages);
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
