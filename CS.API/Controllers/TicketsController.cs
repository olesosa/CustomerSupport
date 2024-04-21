using System.Security.Claims;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using CS.DOM.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IDetailsService _detailsService;
        
        public TicketsController(ITicketService ticketService, IDetailsService detailsService)
        {
            _ticketService = ticketService;
            _detailsService = detailsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TicketFilter filter, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            if (role == "User")
            {
                filter.UserId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            var tickets = await _ticketService.GetAll(filter, cancellationToken);

            return Ok(tickets);
        }

        [Authorize]
        [HttpGet("{ticketId:guid}")]
        public async Task<IActionResult> GetFullInfo(Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketService.GetFullInfoById(ticketId);

            return Ok(ticket);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketCreateDto ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            var createdTicket = await _ticketService.Create(ticket, userId);

            return Ok(createdTicket);
        }

        [Authorize(Policy = "Admin")]
        [HttpPatch("Assign")]
        public async Task<IActionResult> AssignTicket([FromBody] TicketAssignDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsAssigned(dto);

            return Ok(details);
        }

        [Authorize(Policy = "User")]
        [HttpPatch("Solve/{ticketId:guid}")]
        public async Task<IActionResult> MarkAsSolved([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsSolved(ticketId);
            
            return Ok(details);
        }
        
        [Authorize(Policy = "User")]
        [HttpPatch("Close/{ticketId:guid}")]
        public async Task<IActionResult> MarkAsClosed([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsClosed(ticketId);
            
            return Ok(details);
        }
        
    }
}
