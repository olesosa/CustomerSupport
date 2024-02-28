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
                RequestType = ticket.RequestType,
                IsAssigned = ticket.IsAssigned,
                Topic = ticket.Details.Topic,
                Description = ticket.Details.Description,
                WhenCreated = ticket.Details.WhenCreated,
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
                Details = new TicketDetails()
                {
                    Topic = ticketDto.Topic,
                    Description = ticketDto.Description,
                    WhenCreated = ticketDto.WhenCreated,
                },
            };
        }

        public TicketAttachment MapToTicketAttachment(TicketAttachmentDto attachmentDto)
        {
            return new TicketAttachment()
            {
                Id = attachmentDto.Id,
                FilePath = attachmentDto.FilePath,
                TicketId = attachmentDto.TicketId,
            };
        }

        public Ticket MapUpdateTicket(TicketUpdateDto ticketUpdateDto)
        {
            return new Ticket()
            {
                Id = ticketUpdateDto.Id,
                IsAssigned = ticketUpdateDto.IsAssigned,
                IsSolved = ticketUpdateDto.IsSolved,
                RequestType = ticketUpdateDto.RequestType,
            };
        }
    }
}
