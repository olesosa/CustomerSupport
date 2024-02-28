using CS.BL.Interfaces;
using CS.DOM.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllUnAssigned()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tickets = await _ticketService.GetAllUnAsssigned();

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
        [HttpGet("{userId:Guid}")]
        public async Task<IActionResult> GetAllUserTickets([FromRoute] Guid userId)
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{adminId:Guid}")]
        public async Task<IActionResult> GetAllAdminTickets([FromRoute] Guid adminId)
        {
            return Ok();
        }

        [Authorize(Roles = "User")]
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
                return BadRequest(); 
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> AssignTicket([FromRoute] Guid ticketId)
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> UnAssignTicket([FromRoute] Guid ticketId)
        {
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> ReAssignTicket([FromRoute] Guid ticketId, [FromBody]Guid adminId)
        {
            return Ok();
        }

        [Authorize]
        [HttpPut("{ticketId:Guid}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] Guid ticketId)
        {
            return Ok();
        }
    }
}
