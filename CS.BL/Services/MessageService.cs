using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class MessageService : IMessageService
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;

        public MessageService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> Create(Message message)
        {
            await _context.Messages.AddAsync(message);

            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var message = await _context.Messages.FirstOrDefaultAsync(u => u.Id == id);

            if (message != null)
            {
                _context.Messages.Remove(message);

            }

            return await SaveAsync();

        }

        public async Task<List<MessageDto>?> GetAllByDialogId(Guid dialogId)
        {
            return await _context.Messages
                .Include(u => u.Details)
                .Include(u => u.Attachments)
                .Where(m => m.DialogId == dialogId)
                .Select(m => new MessageDto()
                {
                    Id = m.Id,
                    IsRead = m.IsRead,
                    MessageText = m.MessageText,
                    UserId = m.UserId,
                    WhenSended = m.Details.WhenCreated,
                    FilePath = m.Attachments
                    .Select(m => m.FilePath).ToList(),
                }).ToListAsync();
        }

        public async Task<MessageDto?> GetById(Guid id)
        {
            var message = await _context.Messages
                .Include(m => m.Details)
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);

            return new MessageDto()
            {
                Id = message.Id,
                IsRead = message.IsRead,
                MessageText = message.MessageText,
                UserId = message.UserId,
                WhenSended = message.Details.WhenCreated,
                FilePath = message.Attachments
                .Select(m => m.FilePath).ToList(),
            };
        }

        public async Task<bool> Update(Message message)
        {
            _context.Messages.Update(message);

            return await SaveAsync();
        }

    }
}
