using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Services
{
    public class AttachmentService : IAttachmentService
    {
        readonly ApplicationContext _context;
        readonly ICustomMapper _customMapper;

        public AttachmentService(ApplicationContext context, ICustomMapper customMapper)
        {
            _context = context;
            _customMapper = customMapper;
        }

        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> AddAttachment(TicketAttachmentDto attachmentDto)
        {
            var attachment = _customMapper.MapToTicketAttachment(attachmentDto);

            if (attachment != null) 
            {
                await _context.TicketAttachments.AddAsync(attachment);
            }

            return await SaveAsync();
        }

        public async Task<bool> AddAttachment(List<TicketAttachmentDto> attachmentDtos)
        {
            List<TicketAttachment> attachments = attachmentDtos
                .Select(a => _customMapper.MapToTicketAttachment(a))
                .ToList();

            if (attachments != null)
            {
                await _context.TicketAttachments.AddRangeAsync(attachments);
            }

            return await SaveAsync();
        }
    }
}
