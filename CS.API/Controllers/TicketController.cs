using System.Security.Claims;
using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IDetailsService _detailsService;
        private readonly IAttachmentService _attachmentService;
        
        public TicketController(ITicketService ticketService, IDetailsService detailsService, IAttachmentService attachmentService)
        {
            _ticketService = ticketService;
            _detailsService = detailsService;
            _attachmentService = attachmentService;
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
        public async Task<IActionResult> Create([FromBody] TicketCreateDto ticket) // TODO Use claims
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var createdTicket = await _ticketService.Create(ticket, userId);

            return Ok(createdTicket);
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
        public async Task<IActionResult> UnAssignTicket([FromRoute] Guid ticketId)
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
        [HttpPost("Attachment/{ticketId:Guid}")]
        public async Task<IActionResult> AddTicketAttachment( IFormFile file, [FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var filePath= await _attachmentService.AddAttachment(file, ticketId);
            
            return Ok(filePath);
        }

        [Authorize]
        [HttpPatch("Solve")]
        public async Task<IActionResult> MarkAsSolved([FromBody] TicketSolveDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsSolved(ticketDto);
            
            return Ok(details);
        }
        
        [Authorize]
        [HttpPatch("Close")]
        public async Task<IActionResult> MarkAsClosed([FromBody] TicketCloseDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsClosed(ticketDto);
            
            return Ok(details);
        }
        
    }
}
