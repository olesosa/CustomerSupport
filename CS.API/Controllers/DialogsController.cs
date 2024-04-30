using CS.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CS.DOM.Pagination;

namespace CS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DialogsController : ControllerBase
{
    private readonly IDialogService _dialogService;

    public DialogsController(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [Authorize(Policy = "Admin")]
    [HttpPost("{ticketId:Guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid ticketId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var adminId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        var dialog = await _dialogService.Create(ticketId, adminId);

        return Ok(dialog);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DialogFilter filter, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        filter.UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        filter.RoleName = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            
        var dialogs = await _dialogService.GetAllDialogs(filter, cancellationToken);

        return Ok(dialogs);
    }

    [Authorize]
    [HttpGet("{dialogId:guid}")]
    public async Task<IActionResult> GetDialog([FromRoute] Guid dialogId, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
            
        var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            
        var dialog = await _dialogService.GetById(dialogId, userId, cancellationToken);
            
        return Ok(dialog);
    }
}