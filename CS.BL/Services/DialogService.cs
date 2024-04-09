using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
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

        public async Task<DialogDto> GetById(Guid id, CancellationToken cancellationToken = default)
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

            return new DialogDto()
            {
                Id = dialog.Id,
                CustomerId = dialog.Ticket.CustomerId,
                AdminId = dialog.Ticket.AdminId,
                Ticket = await _ticketService.GetFullInfoById(dialog.Ticket.Id, cancellationToken),
                Messages = await _messageService.GetAllByDialogId(dialog.Id, cancellationToken),
            };
        }

        public async Task<List<DialogShortInfoDto>> GetAllUserDialogs(Guid userId, string roleName)
        {
            var dialogs = _context.Dialogs
                .Include(d => d.Ticket)
                .ThenInclude(t => t.Customer)
                .Include(dialog => dialog.Messages);

            switch (roleName)
            {
                case "User":
                    await dialogs
                        .Where(d => d.Ticket.AdminId == userId)
                        .ToListAsync();

                    break;
                
                case "Admin":
                    await dialogs
                        .Where(d => d.Ticket.CustomerId == userId)
                        .ToListAsync();
                    
                    break;
            }

            if (dialogs == null)
            {
                throw new ApiException(404, "User has no dialogs");
            }

            return dialogs.Select(d => new DialogShortInfoDto()
            {
                Id = d.Id,
                LastMassage = d.Messages.LastOrDefault().ToString(),
                UserName = roleName == "Admin" ? d.Ticket.Customer.Name : d.Ticket.Admin.Name
            }).ToList();
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

            return _customMapper.MapDialogCreate(dialog, ticket);
        }
    }
}
