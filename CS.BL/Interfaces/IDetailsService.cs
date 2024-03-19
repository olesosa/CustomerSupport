using CS.DOM.DTO;

namespace CS.BL.Interfaces;

public interface IDetailsService
{
    Task<DetailsPatchDto> MarkAsSolved(TicketSolveDto ticketDto);
    Task<DetailsPatchDto> MarkAsClosed(TicketCloseDto ticketDto);
    
}