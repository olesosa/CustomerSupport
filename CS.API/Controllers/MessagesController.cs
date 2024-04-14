using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

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
        [HttpGet("messages/{dialogId:Guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid dialogId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var messages = await _messageService.GetAll(dialogId, cancellationToken);

            return Ok(messages);
        }

    }
}
