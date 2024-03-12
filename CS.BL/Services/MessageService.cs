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
        private readonly ICustomMapper _customMapper;

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

            if (message == null)
            {
                throw new Exception("Message null ref");
            }
            
            return _customMapper.MapToMessageDto(message);
        }

        public async Task<MessageDto> SendMessage(SendMessageDto messageDto, Guid senderId)
        {
            var message = _mapper.Map<Message>(messageDto);

            if (message == null)
            {
                throw new Exception("Message null ref");
            }

            message.UserId = senderId;
            
            await _context.Messages.AddAsync(message);
            
            return _customMapper.MapToMessageDto(message);
        }
    }
}
