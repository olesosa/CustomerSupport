using CS.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CS.DOM.Helpers;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        private readonly IDialogService _dialogService;
        private readonly ITicketService _ticketService;

        public DialogsController(IDialogService dialogService, ITicketService ticketService)
        {
            _dialogService = dialogService;
            _ticketService = ticketService;
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("{ticketId:Guid}")]
        public async Task<IActionResult> Create([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var adminId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                if (!await _ticketService.IsTicketExist(ticketId))
                {
                    return NotFound("Ticket does not exist");
                }

                var dialog = _dialogService.Create(ticketId, adminId);

                return Ok(dialog);
            }
            catch
            {
                throw new ApiException(500, "Can not create dialog");
            }
            
        }
    }
}