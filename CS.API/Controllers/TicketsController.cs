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
        [HttpGet("{number:int}")]
        public async Task<IActionResult> GetFullInfo([FromRoute] int number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketService.GetFullInfo(number);

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

        [Authorize(Roles = "User")]
        [HttpPut("{ticketId:guid}")]
        public async Task<IActionResult> UpdateTicket([FromBody] TicketUpdateDto ticket, [FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTicket = await _detailsService.UpdateTicketDetails(ticket, ticketId);

            return Ok(updatedTicket);
        }
        
        [Authorize(Policy = "Admin")]
        [HttpPatch("Assign/{ticketId:guid}")]
        public async Task<IActionResult> AssignTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = new TicketAssignDto()
            {
                TicketId = ticketId,
                AdminId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            
            var details = await _detailsService.MarkAsAssigned(ticket);

            return Ok(details);
        }

        [Authorize(Policy = "Admin")]
        [HttpPatch("Reassign")]
        public async Task<IActionResult> ReAssignTicket([FromBody] TicketAssignDto ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsAssigned(ticket);

            return Ok(details);
        }
        
        [Authorize(Policy = "User")]
        [HttpPatch("Solve/{ticketId:guid}")]
        public async Task<IActionResult> SolveTicket([FromRoute] Guid ticketId)
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
        public async Task<IActionResult> CloseTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsClosed(ticketId);
            
            return Ok(details);
        }

        [Authorize(Policy = "Admin")]
        [HttpPatch("Receive/{number:int}")]
        public async Task<IActionResult> ReceiveTicket([FromRoute] int number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsReceived(number);

            return Ok(details);
        }

        [Authorize]
        [HttpGet("Statistic")]
        public async Task<IActionResult> GetStatistic([FromQuery] StatisticFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            filter.UserId = role == "User" ? userId : null;
            
            var statistic = await _ticketService.GetTicketsStatistic(filter);
            
            return Ok(statistic);
        }
        
    }
}
