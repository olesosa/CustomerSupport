using CS.BL.Interfaces;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Helpers
{
    public class CustomMapper : ICustomMapper
    {
        public TicketFullInfoDto MapToTicketFullInfo(Ticket ticket)
        {
            return new TicketFullInfoDto()
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                RequestType = ticket.RequestType,
                Topic = ticket.Topic,
                IsAssigned = ticket.IsAssigned,
                Description = ticket.Details.Description,
                IsClosed =ticket.Details.IsClosed,
                IsSolved = ticket.Details.IsSolved,
                CreationTime = ticket.Details.CreationTime,
                AttachmentsFilePath = ticket.Attachments
                .Select(t => t.FilePath).ToList(),
            };
        }

        public Ticket MapToTicket(TicketCreateDto ticketDto)
        {
            return new Ticket()
            {
                RequestType = ticketDto.RequestType,
                Topic = ticketDto.Topic,
                IsAssigned = false,
                Details = new TicketDetails()
                {
                    Description = ticketDto.Description,
                    CreationTime = ticketDto.CreationTime,
                    IsSolved = false,
                    IsClosed = false,
                },
            };
        }

        public MessageDto MapToMessageDto(Message message)
        {
            return new MessageDto()
            {
                Id = message.Id,
                IsRead = message.IsRead,
                MessageText = message.MessageText,
                UserId = message.UserId,
                WhenSended = message.WhenSend,
                FilePath = message.Attachments
                .Select(m => m.FilePath).ToList(),
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

        public DetailsPatchDto MapDetails(Ticket ticket)
        {
            return new DetailsPatchDto()
            {
                TicketId = ticket.Id,
                IsSolved = ticket.Details.IsSolved,
                IsClosed = ticket.Details.IsClosed,
            };
        }
    }
}
