using CS.BL.Interfaces;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Helpers
{
    //TODO: Why not automapper?
    public class CustomMapper : ICustomMapper
    {
        public TicketFullInfoDto MapToTicketFullInfo(Ticket ticket)
        {
            return new TicketFullInfoDto()
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                Number = ticket.Number,
                RequestType = ticket.RequestType,
                Topic = ticket.Topic,
                Description = ticket.Details.Description,
                AttachmentIds = ticket.Attachments.Select(a=>a.Id).ToList(),
                IsAssigned = ticket.Details.IsAssigned,
                IsClosed =ticket.Details.IsClosed,
                IsSolved = ticket.Details.IsSolved,
                CreationTime = ticket.Details.CreationTime
            };
        }
        

        public DialogCreateDto MapDialogCreate(Dialog dialog, Ticket ticket)
        {
            return new DialogCreateDto()
            {
                TicketId = dialog.TicketId,
                CustomerId = ticket.CustomerId,
                AdminId = ticket.AdminId,
            };
        }
    }
}
