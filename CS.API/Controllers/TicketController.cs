using CS.BL.Interfaces;
using CS.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CS.API.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet("GetAllTickets")]
        public async Task<IActionResult> GetAllTickets() 
        {
            var tickets = await _ticketService.GetAll();

            if (tickets != null)
            {
                return Ok(tickets);
            }
            else
            {
                return NotFound("No tickets out there");
            }
        }
    }
}
