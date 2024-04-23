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
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public MessageService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MessageDto>> GetAll(Guid dialogId, CancellationToken cancellationToken = default)
        {
            var messages = await _context.Messages
                .Include(u => u.Attachments)
                .Where(m => m.DialogId == dialogId)
                .ToListAsync(cancellationToken);

            var messagesDto = messages.Select(m => new MessageDto()
            {
                DialogId = m.DialogId,
                UserId = m.UserId,
                Text = m.MessageText,
                WhenSended = m.WhenSend,
                UserName = _context.Users.FirstOrDefault(u=> u.Id == m.UserId)!.UserName,
                Attachments = m.Attachments.Select(a => a.Id).ToList()
            }).ToList();
            
            return messagesDto;
        }
        
        public async Task<Message> SaveMessage(string text, Guid dialogId, Guid userId)
        {
            var message = new Message()
            {
                DialogId =  dialogId,
                MessageText = text,
                WhenSend = DateTime.Now,
                IsRead = false,
                UserId = userId
            };

            await _context.Messages.AddAsync(message);

            await _context.SaveChangesAsync();

            return message;
        }

        public async Task MarkAsRead(Guid dialogId)
        {
            var messages = await _context.Dialogs
                .Include(d => d.Messages)
                .Where(d => d.Id == dialogId)
                .SelectMany(d => d.Messages)
                .ToListAsync();

            foreach (var message in messages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();
        }
    }
}
