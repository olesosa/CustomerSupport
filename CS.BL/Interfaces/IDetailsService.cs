using CS.DOM.DTO;

namespace CS.BL.Interfaces;

public interface IDetailsService
{
    Task<TicketPatchDto> MarkAsAssigned(TicketAssignDto ticketDto);
    Task<TicketPatchDto> MarkAsSolved(Guid ticketId);
    Task<TicketPatchDto> MarkAsClosed(Guid ticketId);
    
}