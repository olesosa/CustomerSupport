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
    public async Task<IActionResult> AddTicketAttachment(IFormCollection files, [FromRoute] Guid ticketId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (files.Files.Count == 0)
        {
            return NoContent();
        }

        var filesId = new List<Guid>();
        
        foreach (var file in files.Files)
        {
            filesId.Add(await _attachmentService.AddTicketAttachment(file, ticketId));
        }
        
        return Ok(filesId);
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
    public async Task<IActionResult> AddMessageAttachment(IFormCollection files, [FromRoute] Guid messageId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (files.Files.Count == 0)
        {
            return NoContent();
        }

        var filesId = new List<Guid>();
        
        foreach (var file in files.Files)
        {
            filesId.Add(await _attachmentService.AddMessageAttachment(file, messageId));
        }

        return Ok(filesId);
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
}