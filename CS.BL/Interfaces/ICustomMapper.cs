using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ICustomMapper
    {
        TicketFullInfoDto MapToTicketFullInfo(Ticket ticket);
        TicketShortInfoDto MapToTicketShortInfo(Ticket ticket);
        Ticket MapToTicket(TicketCreateDto ticketDto);
        MessageDto MapToMessageDto(Message message);
    }
}
