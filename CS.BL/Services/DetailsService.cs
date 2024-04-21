using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services;

public class DetailsService : IDetailsService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;

    public DetailsService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<TicketPatchDto> MarkAsAssigned(TicketAssignDto ticketDto)
    {
        var detail = await _context.TicketDetails
            .FirstOrDefaultAsync(d => d.TicketId == ticketDto.TicketId);
        
        if (detail == null)
        {
            throw new ApiException(404, "Ticket detail not found");
        }
        
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.Id == detail.TicketId);

        if (ticket == null)
        {
            throw new ApiException(404, "Ticket not found");
        }

        if (!detail.IsAssigned)
        {
            detail.AssignmentTime = DateTime.Now;
            ticket.AdminId = ticketDto.AdminId;
            detail.HasReceived = false;
        }
        else
        {
            detail.AssignmentTime = null;
            ticket.AdminId = null;
            detail.HasReceived = null;
        }

        detail.IsAssigned = !detail.IsAssigned;
        
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TicketPatchDto>(detail);
    }

    public async Task<TicketPatchDto> MarkAsSolved(Guid ticketId)
    {
        var detail = await _context.TicketDetails
            .FirstOrDefaultAsync(d => d.TicketId == ticketId);

        if (detail == null)
        {
            throw new ApiException(404, "Ticket not found");
        }

        detail.IsSolved = !detail.IsSolved;
        
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TicketPatchDto>(detail);
    }

    public async Task<TicketPatchDto> MarkAsClosed(Guid ticketId)
    {
        var detail = await _context.TicketDetails
            .FirstOrDefaultAsync(d => d.TicketId == ticketId);

        if (detail == null)
        {
            throw new ApiException(404, "Ticket not found");
        }

        detail.IsSolved = !detail.IsSolved;

        await _context.SaveChangesAsync();
        
        return _mapper.Map<TicketPatchDto>(detail);
    }
}