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

        //[Authorize(Roles = "Admin")]
        [HttpPost("Tickets")]
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
        [HttpPost("Create")]
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

        [Authorize(Roles = "Admin")]
        [HttpPatch("Assign/{ticketId:Guid}")]
        public async Task<IActionResult> AssignTicket([FromBody] AssignTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = await _ticketService.AssignTicket(ticketDto.ticketId, ticketDto.adminId);

            return Ok(ticket);
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("UnAssign/{ticketId:Guid}")]
        public async Task<IActionResult> UnAssignTicket([FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketService.UnAssignTicket(ticketId);

            return Ok(ticket);
        }
        
        [Authorize]
        [HttpPost("Attachment/{ticketId:Guid}")]
        public async Task<IActionResult> AddTicketAttachment(IFormFile file, [FromRoute] Guid ticketId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileId= await _attachmentService.AddTicketAttachment(file, ticketId);
            
            return Ok(fileId);
        }
        
        [Authorize]
        [HttpGet("Attachment/{ticketId:Guid}")]
        public async Task<IActionResult> AddTicketAttachment([FromRoute] Guid attachmentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attachment = await _attachmentService.GetTicketAttachment(attachmentId);

            return File(attachment.FileBytes, attachment.ContentType, attachment.FilePath);
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
