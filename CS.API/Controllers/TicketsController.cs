using System.Security.Claims;
using CS.BL.Interfaces;
using CS.DOM.DTO;
using CS.DOM.Helpers;
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
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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

        [Authorize(Policy = "User")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TicketCreateDto ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                var createdTicket = await _ticketService.Create(ticket, userId);

                return Ok(createdTicket);
            }
            catch
            {
                throw new ApiException(400, "Can not create ticket");
            }

        }

        [Authorize(Policy = "Admin")]
        [HttpPatch("assign/{ticketId:Guid}")]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketService.AssignTicket(ticketDto.ticketId, ticketDto.adminId);

            return Ok(ticket);
        }

        [Authorize(Policy = "Admin")]
        [HttpPatch("unassign/{ticketId:Guid}")]
        public async Task<IActionResult> UnAssignTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketService.UnAssignTicket(ticketId);

            return Ok(ticket);
        }

        [Authorize(Policy = "User")]
        [HttpPatch("solve")]
        public async Task<IActionResult> MarkAsSolved([FromBody] TicketSolveDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var details = await _detailsService.MarkAsSolved(ticketDto);
            
            return Ok(details);
        }
        
        [Authorize(Policy = "User")]
        [HttpPatch("close")]
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
