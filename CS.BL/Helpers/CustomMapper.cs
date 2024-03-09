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
                IsAssigned = ticket.IsAssigned,
                Topic = ticket.Details.Topic,
                Description = ticket.Details.Description,
                WhenCreated = ticket.Details.CreationTime,
                AttachmentsFilePath = ticket.Attachments
                .Select(t => t.FilePath).ToList(),
            };
        }

        public TicketShortInfoDto MapToTicketShortInfo(Ticket ticket)
        {
            return new TicketShortInfoDto()
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                RequestType = ticket.RequestType,
                IsAssigned = ticket.IsAssigned,
                Topic = ticket.Details.Topic,
            };
        }

        public Ticket MapToTicket(TicketCreateDto ticketDto)
        {
            return new Ticket()
            {
                Id = ticketDto.Id,
                CustomerId = ticketDto.CustomerId,
                RequestType = ticketDto.RequestType,
                Details = new TicketDetails()
                {
                    Topic = ticketDto.Topic,
                    Description = ticketDto.Description,
                    CreationTime = ticketDto.CreationTime,
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
    }
}
