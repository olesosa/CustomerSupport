﻿using CS.BL.Interfaces;
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
                Number = ticket.Number,
                RequestType = ticket.RequestType,
                Topic = ticket.Topic,
                Description = ticket.Details.Description,
                IsAssigned = ticket.Details.IsAssigned,
                IsClosed =ticket.Details.IsClosed,
                IsSolved = ticket.Details.IsSolved,
                CreationTime = ticket.Details.CreationTime
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
