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
    private readonly IDialogService _dialogService;

    public DetailsService(ApplicationContext context, IMapper mapper, IDialogService dialogService)
    {
        _context = context;
        _mapper = mapper;
        _dialogService = dialogService;
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

        detail.AssignmentTime = DateTime.Now;
        ticket.AdminId = ticketDto.AdminId;
        detail.HasReceived = false;

        detail.IsAssigned = true;

        await _dialogService.Create(ticket.Id, (Guid)ticket.AdminId);

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

    public async Task<TicketPatchDto> MarkAsReceived(int number)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Details)
            .FirstOrDefaultAsync(t => t.Number == number);

        if (ticket == null)
        {
            throw new ApiException(404, "Ticket not found");
        }

        ticket.Details.HasReceived = true;

        await _context.SaveChangesAsync();

        return _mapper.Map<TicketPatchDto>(ticket.Details);
    }

    public async Task<TicketPatchDto> UpdateTicketDetails(TicketUpdateDto ticket, Guid ticketId)
    {
        var detail = await _context.TicketDetails
            .FirstOrDefaultAsync(d => d.TicketId == ticketId);

        if (detail == null)
        {
            throw new ApiException(404, "Ticket not found");
        }

        detail.IsSolved = ticket.IsSolved;

        detail.IsClosed = ticket.IsClosed;

        await _context.SaveChangesAsync();

        return _mapper.Map<TicketPatchDto>(detail);
    }
}