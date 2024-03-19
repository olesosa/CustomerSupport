using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services;

public class DetailsService : IDetailsService
{
    private readonly ApplicationContext _context;
    private readonly ICustomMapper _customMapper;

    public DetailsService(ApplicationContext context, ICustomMapper customMapper)
    {
        _context = context;
        _customMapper = customMapper;
    }

    public async Task<DetailsPatchDto> MarkAsSolved(TicketSolveDto ticketDto)
    {
        var ticket = await _context.Tickets
            .Include(t=>t.Details)
            .FirstOrDefaultAsync(t => t.Id == ticketDto.TicketId);

        if (ticket != null && ticket.Details != null)
        {
            ticket.Details.IsSolved = ticketDto.IsSolved;
            
            return _customMapper.MapDetails(ticket);
        }

        throw new Exception("Ticket not Found");
    }

    public async Task<DetailsPatchDto> MarkAsClosed(TicketCloseDto ticketDto)
    {
        var ticket = await _context.Tickets
            .Include(t=>t.Details)
            .FirstOrDefaultAsync(t => t.Id == ticketDto.TicketId);

        if (ticket != null && ticket.Details != null)
        {
            ticket.Details.IsClosed = ticketDto.IsClosed;
            
            return _customMapper.MapDetails(ticket);
        }

        throw new Exception("Ticket not Found");
    }
}