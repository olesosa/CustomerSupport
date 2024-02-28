using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace CS.BL.Services
{
    public class MassageService : BaseService, IMessageService
    {
        public MassageService(ApplicationContext context) : base(context) { }

        public async Task<bool> Create(Message message)
        {
            await _context.Messages.AddAsync(message);

            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            _context.Messages.Remove(await _context.Messages.FirstOrDefaultAsync(u => u.Id == id));

            return await SaveAsync();

        }

        public async Task<DTOMessage?> GetById(Guid id)
        {
            var message = await _context.Messages
                .Include(m => m.Details)
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id);

            return new DTOMessage()
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
