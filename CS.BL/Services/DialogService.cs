using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ICustomMapper _customMapper;
        private readonly ITicketService _ticketService;
        private readonly IMessageService _messageService;

        public DialogService(ApplicationContext context, IMapper mapper, ICustomMapper customMapper,
            ITicketService ticketService, IMessageService messageService)
        {
            _context = context;
            _mapper = mapper;
            _customMapper = customMapper;
            _ticketService = ticketService;
            _messageService = messageService;
        }

        public async Task<DialogDto> GetById(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var dialog = await _context.Dialogs
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

            return new DialogDto()
            {
                Id = dialog.Id,
                Ticket = await _ticketService.GetFullInfoById(dialog.Ticket.Id, cancellationToken),
                Messages = await _messageService.GetAll(dialog.Id, cancellationToken),
            };
        }

        public async Task<List<DialogShortInfoDto>> GetAllDialogs(DialogFilter filter,
            CancellationToken cancellationToken)
        {
            var dialogsQuery = _context.Dialogs
                .Include(d => d.Ticket)
                .ThenInclude(t => t.Customer)
                .Include(dialog => dialog.Messages)
                .Include(dialog => dialog.Ticket)
                .ThenInclude(ticket => ticket.Admin)
                .AsQueryable();

            if (filter.RoleName == "User")
            {
                dialogsQuery = dialogsQuery.Where(d => d.Ticket.CustomerId == filter.UserId);
            }
            else if (filter.RoleName == "Admin")
            {
                dialogsQuery = dialogsQuery.Where(d => d.Ticket.AdminId == filter.UserId);
            }
            else
            {
                throw new ApiException(400, "Invalid role name");
            }

            if (filter.SortDir == "asc")
            {
                if (filter.DateTime.HasValue)
                {
                    dialogsQuery = dialogsQuery
                        .OrderBy(d => d.Messages.OrderBy(m => m.WhenSend).LastOrDefault());
                }
            }
            else
            {
                if (filter.DateTime.HasValue)
                {
                    dialogsQuery = dialogsQuery
                        .OrderByDescending(d => d.Messages.OrderBy(m => m.WhenSend).LastOrDefault());
                }
            }

            var dialogs = await dialogsQuery.ToListAsync(cancellationToken);

            if (dialogsQuery == null)
            {
                throw new ApiException(404, "User has no dialogs");
            }
            
            var dialogDto = dialogs.Select(dialog => new DialogShortInfoDto
            {
                Id = dialog.Id,
                LastMassage = dialog.Messages.Any() ? dialog.Messages.Last().MessageText : "No messages yet"
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