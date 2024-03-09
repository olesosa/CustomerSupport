using CS.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogController : ControllerBase
    {
        private readonly IDialogService _dialogService;
        private readonly ITicketService _ticketService;

        public DialogController(IDialogService dialogService, ITicketService ticketService)
        {
            _dialogService = dialogService;
            _ticketService = ticketService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{ticketId:Guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guid.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid adminId);

            if (!await _ticketService.IsTicketExist(ticketId))
            {
                return NotFound("Ticket does not exist");
            }

            var dialog = _dialogService.Create(ticketId, adminId);

            if (dialog == null)
            {
                return BadRequest("Can not create dialog");
            }

            return Ok(dialog);
        }
    }
}