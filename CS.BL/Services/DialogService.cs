using AutoMapper;
using CS.BL.Extensions;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using CS.DOM.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class DialogService : IDialogService
    {
        private readonly ApplicationContext _context;
        private readonly ICustomMapper _customMapper;
        private readonly ITicketService _ticketService;//not used
        private readonly IMessageService _messageService;

        public DialogService(ApplicationContext context, ICustomMapper customMapper,
            ITicketService ticketService, IMessageService messageService)
        {
            _context = context;
            _customMapper = customMapper;
            _ticketService = ticketService;
            _messageService = messageService;
        }

        public async Task<DialogDto> GetById(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var dialog = await _context.Dialogs//some of included entities are not used. performance issue
                .Include(d => d.Messages).ThenInclude(m => m.Attachments)
                .Include(d => d.Ticket).ThenInclude(t => t.Details)
                .Include(d => d.Ticket).ThenInclude(t => t.Attachments)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

            if (dialog == null)
            {
                throw new ApiException(404, "Dialog not found");
            }

            if (dialog.Ticket.CustomerId != userId && dialog.Ticket.AdminId != userId)
            {
                throw new ApiException(403, "No access to dialog");
            }

            var ticket = new DialogTicketDto()
            {
                Id = dialog.TicketId,
                CustomerId = dialog.Ticket.CustomerId,
                AdminId = (Guid)dialog.Ticket.AdminId!,
                Number = dialog.Ticket.Number,
                Request = dialog.Ticket.RequestType,
                Topic = dialog.Ticket.Topic
            };
            
            return new DialogDto()
            {
                Id = dialog.Id,
                Ticket = ticket,
                Messages = await _messageService.GetAll(dialog.Id, cancellationToken),
            };
        }

        public async Task<List<DialogShortInfoDto>> GetAllDialogs(DialogFilter filter,
            CancellationToken cancellationToken)
        {
            var dialogs = await _context.Dialogs
                .Include(d => d.Ticket)
                .ThenInclude(t => t.Customer)
                .Include(dialog => dialog.Messages)
                .Include(dialog => dialog.Ticket)
                .ThenInclude(ticket => ticket.Admin)
                .AsQueryable()//line not needed
                .PaginateDialogs(filter, cancellationToken);
            
            if (dialogs == null)
            {
                throw new ApiException(404, "User has no dialogs");
            }
            
            var dialogDto = dialogs.Select(dialog => new DialogShortInfoDto//can be simplified to simple return statement
            {
                Id = dialog.Id,
                LastMassage = dialog.Messages.Any() ? dialog.Messages.Last().MessageText : "No messages yet",
                Number = dialog.Ticket.Number,
                IsRead = dialog.Messages.Any(m => !m.IsRead)
            }).ToList();

            return dialogDto;
        }

        public async Task<DialogCreateDto> Create(Guid ticketId, Guid adminId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket == null)
            {
                throw new Exception("Invalid ticket id, can not create dialog");
            }

            ticket.AdminId = adminId;

            var dialog = new Dialog()
            {
                TicketId = ticket.Id,
            };

            await _context.Dialogs.AddAsync(dialog);

            await _context.SaveChangesAsync();

            return _customMapper.MapDialogCreate(dialog, ticket);
        }
    }
}