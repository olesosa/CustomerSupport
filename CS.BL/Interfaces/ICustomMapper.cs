using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ICustomMapper
    {
        TicketFullInfoDto MapToTicketFullInfo(Ticket ticket);
        MessageDto MapToMessageDto(Message message);
        DialogCreateDto MapDialogCreate(Dialog dialog, Ticket ticket);
        DetailsPatchDto MapDetails(Ticket ticket);
    }
}
