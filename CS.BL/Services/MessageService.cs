using AutoMapper;
using CS.BL.Hubs;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using Microsoft.AspNetCore.SignalR;
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

            var messageDtos = messages.Select(m => new MessageDto()
            {
                Id = m.Id,
                UserId = m.UserId,
                Text = m.MessageText,
                WhenSended = m.WhenSend,
                UserName = _context.Users.FirstOrDefault(u=> u.Id == m.UserId).Name,
                AttachmentIds = m.Attachments.Select(a => a.Id).ToList()
            }).ToList();
            
            return messageDtos;
        }
    }
}
