using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{dialogId:Guid}")]
        public async Task<IActionResult> GetAllDialogMessages([FromRoute] Guid dialogId) 
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto message)
        {
            return Ok();
        }
    }
}
