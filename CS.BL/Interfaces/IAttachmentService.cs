using CS.DOM.DTO;
using Microsoft.AspNetCore.Http;

namespace CS.BL.Interfaces
{
    public interface IAttachmentService
    {
        Task<Guid> AddTicketAttachment(IFormFile file, Guid ticketId);
        Task<Guid> AddMessageAttachment(IFormFile file, Guid ticketId);
        Task<AttachmentGetDto> GetTicketAttachment(Guid attachmentId);
        Task<AttachmentGetDto> GetMessageAttachment(Guid attachmentId);
    }
}
