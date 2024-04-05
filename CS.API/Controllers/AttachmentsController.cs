using CS.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;

    public AttachmentsController(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    [HttpPost("ticket/{ticketId:Guid}")]
    public async Task<IActionResult> AddTicketAttachment(IFormFile file, [FromRoute] Guid ticketId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var fileId = await _attachmentService.AddTicketAttachment(file, ticketId);

        return Ok(fileId);
    }

    [HttpGet("ticket/{attachmentId:Guid}")]
    public async Task<IActionResult> GetTicketAttachment([FromRoute] Guid attachmentId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var attachment = await _attachmentService.GetTicketAttachment(attachmentId);

        return File(attachment.FileBytes, attachment.ContentType, attachment.FilePath);
    }

    [HttpPost("message/{messageId:Guid}")]
    public async Task<IActionResult> AddMessageAttachment(IFormFile file, [FromRoute] Guid messageId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var filePath = await _attachmentService.AddMessageAttachment(file, messageId);

        return Ok(filePath);
    }

    [HttpGet("message/{attachmentId:Guid}")]
    public async Task<IActionResult> GetMessageAttachment([FromRoute] Guid attachmentId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var attachment = await _attachmentService.GetMessageAttachment(attachmentId);

        return File(attachment.FileBytes, attachment.ContentType, attachment.FilePath);
    }

    [HttpGet("{dialogId:guid}")]
    public async Task<IActionResult> GetAll(Guid dialogId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var attachments = await _attachmentService.GetAllDialogAttachments(dialogId);

        return Ok(attachments);
    }
}