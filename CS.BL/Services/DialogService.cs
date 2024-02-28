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
        readonly ITicketService _ticketService;
        readonly IMessageService _messageService;

        public DialogService(ApplicationContext context, IMapper mapper, ITicketService ticketService, IMessageService messageService) 
        {
            _context = context;
            _mapper = mapper;
            _ticketService = ticketService;
            _messageService = messageService;
        }

        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> Create(Dialog dialog)
        {
            await _context.Dialogs.AddAsync(dialog);

            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            _context.Dialogs.Remove(await _context.Dialogs.FirstOrDefaultAsync(d => d.Id == id));
            
            return await SaveAsync();
        }

        public async Task<DialogDto?> GetById(Guid id)
        {
            return _mapper.Map<DialogDto?>(
                await _context.Dialogs
                .Include(d => d.Messages).ThenInclude(m => m.Details)
                .Include(d => d.Messages).ThenInclude(m => m.Attachments)
                .Include(d => d.Ticket).ThenInclude(t => t.Details)
                .Include(d => d.Ticket).ThenInclude(t => t.Attachments)
                .FirstOrDefaultAsync(d => d.Id == id));

            //var dialog = await _context.Dialogs
            //    .Include(d => d.Messages).ThenInclude(m => m.Details)
            //    .Include(d => d.Messages).ThenInclude(m => m.Attachments)
            //    .Include(d => d.Ticket).ThenInclude(t => t.Details)
            //    .Include(d => d.Ticket).ThenInclude(t => t.Attachments)
            //    .FirstOrDefaultAsync(d => d.Id == id);

            //return new DialogDto()
            //{
            //    Id = dialog.Id,
            //    CustomerId = dialog.Ticket.CustomerId,
            //    AdminId = dialog.Ticket.AdminId,
            //    Ticket = await _ticketService.GetById(dialog.Ticket.Id),
            //    Messages = await _messageService.GetAllByDialogId(dialog.Id),
            //};
        }

        public async Task<bool> Update(Dialog dialog)
        {
            _context.Dialogs.Update(dialog);

            return await SaveAsync();
        }
    }
}
