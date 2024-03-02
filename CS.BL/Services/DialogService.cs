using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class DialogService : IDialogService
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;
        readonly ICustomMapper _customMapper;
        readonly ITicketService _ticketService;
        readonly IMessageService _messageService;

        public DialogService(ApplicationContext context, IMapper mapper, ICustomMapper customMapper,
            ITicketService ticketService, IMessageService messageService) 
        {
            _context = context;
            _mapper = mapper;
            _customMapper = customMapper;
            _ticketService = ticketService;
            _messageService = messageService;
        }

        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var dialog = await _context.Dialogs.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

            if (dialog != null)
            {
                _context.Dialogs.Remove(dialog);
            }

            return await SaveAsync();
        }

        public async Task<DialogDto?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var dialog = await _context.Dialogs
                .Include(d => d.Messages).ThenInclude(m => m.Attachments)
                .Include(d => d.Ticket).ThenInclude(t => t.Details)
                .Include(d => d.Ticket).ThenInclude(t => t.Attachments)
                .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

            return new DialogDto()
            {
                Id = dialog.Id,
                CustomerId = dialog.Ticket.CustomerId,
                AdminId = dialog.Ticket.AdminId,
                Ticket = await _ticketService.GetById(dialog.Ticket.Id, cancellationToken),
                Messages = await _messageService.GetAllByDialogId(dialog.Id, cancellationToken),
            };
        }

        public async Task<bool> Update(Dialog dialog)
        {
            _context.Dialogs.Update(dialog);

            return await SaveAsync();
        }

        public Task<bool> Create(DialogCreateDto dialogDto)
        {
            throw new NotImplementedException();
        }
    }
}
