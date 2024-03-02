using CS.API.Filters;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
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
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody]TicketCreateDto ticket)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _ticketService.Create(ticket))
            {
                return Ok();
            }
            else 
            { 
                return BadRequest("Ticket can not be created"); 
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> AssignTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid adminId;

            if (Guid.TryParse(Id, out adminId))
            {
                return BadRequest("Id is not valid");
            }
            else if(await _ticketService.AssignTicket(ticketId, adminId))
            {
                return Ok();
            }
            else
            {
                return NotFound("Invalid ticket id");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> UnAssignTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _ticketService.UnAssignTicket(ticketId))
            {
                return Ok();
            }
            else
            {
                return NotFound("Invalid ticket id");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> ReAssignTicket([FromBody] AssignTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _ticketService.AssignTicket(ticketDto.ticketId, ticketDto.adminId))
            {
                return Ok();
            }
            else
            {
                return NotFound("Invalid information");
            }
        }

        [Authorize]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(await _ticketService.Delete(ticketId))
            {
                return Ok("Ticket was successfully deleted");
            }
            else
            {
                return NotFound("Your ticket not be found");
            }
        }
    }
}
