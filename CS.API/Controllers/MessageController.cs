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
        private readonly IAttachmentService _attachmentService;

        public MessageController(IMessageService messageService, IAttachmentService attachmentService)
        {
            _messageService = messageService;
            _attachmentService = attachmentService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Messages/{dialogId:Guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid dialogId, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var messages = _messageService.GetAllByDialogId(dialogId, cancellationToken);

            return Ok(messages);
        }

        [Authorize]
        [HttpPost("SendMessage")]
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

        [Authorize]
        [HttpPost("Attachment/{messageId:Guid}")]
        public async Task<IActionResult> AddMessageAttachment(IFormFile file, [FromRoute] Guid messageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filePath= await _attachmentService.AddMessageAttachment(file, messageId);

            return Ok(filePath);
        }
        
        [Authorize]
        [HttpGet("Attachment/{messageId:Guid}")]
        public async Task<IActionResult> GetMessageAttachment([FromRoute] Guid messageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attachment= await _attachmentService.GetMessageAttachment(messageId);

            return File(attachment.FileBytes, attachment.ContentType, attachment.FilePath);
        }
    }
}
