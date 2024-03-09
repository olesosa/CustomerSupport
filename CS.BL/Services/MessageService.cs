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
        readonly ICustomMapper _customMapper;

        public MessageService(ApplicationContext context, IMapper mapper, ICustomMapper customMapper)
        {
            _context = context;
            _mapper = mapper;
            _customMapper = customMapper;
        }
        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<List<MessageDto>?> GetAllByDialogId(Guid dialogId, CancellationToken cancellationToken = default)
        {
            var messages = await _context.Messages
                .Include(u => u.Attachments)
                .Where(m => m.DialogId == dialogId)
                .ToListAsync(cancellationToken);

            return messages.Select(m => _customMapper.MapToMessageDto(m)).ToList();
        }

        public async Task<MessageDto?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var message = await _context.Messages
                .Include(m => m.Attachments)
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

            return _customMapper.MapToMessageDto(message);
        }

        public async Task<bool> SendMessage(SendMessageDto messageDto)
        {
            var message = _mapper.Map<Message>(messageDto);

            if (message != null)
            {
                await _context.Messages.AddAsync(message);
            }

            return await SaveAsync();
        }
    }
}
