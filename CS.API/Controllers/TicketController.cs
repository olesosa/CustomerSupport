using CS.API.Filters;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Tickets")]
        public async Task<IActionResult> GetAll([FromBody] TicketFilter filter, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tickets = await _ticketService.GetAll(filter, cancellationToken);

            if (tickets != null)
            {
                return Ok(tickets);
            }
            else
            {
                return NotFound("No UnAssigned Tickets Left");
            }
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] TicketCreateDto ticket)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _ticketService.Create(ticket))
            {
                return Ok("Ticket was created");
            }
            else
            {
                return BadRequest("Ticket can not be created");
            }
        }
        
        [Authorize]
        [HttpDelete("{ticketId:Guid}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _ticketService.Delete(ticketId))
            {
                return Ok("Ticket was successfully deleted");
            }
            else
            {
                return NotFound("Your ticket can not be deleted");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("Assign/{ticketId:Guid}")]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _ticketService.AssignTicket(ticketDto.ticketId, ticketDto.adminId))
            {
                return Ok("Ticket has been assigned");
            }
            else
            {
                return NotFound("Invalid ticket id");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("UnAssign/{ticketId:Guid}")]
        public async Task<IActionResult> UnAssign([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _ticketService.UnAssignTicket(ticketId))
            {
                return Ok("Ticket has been assigned");
            }
            else
            {
                return NotFound("Invalid ticket id");
            }
        }
        
        [Authorize]
        [HttpPatch("Attachment")]
        public async Task<IActionResult> AddAttachment([FromBody] TicketAttachmentDto attachmentDto)
        {
            return Ok();
        }
    }
}
