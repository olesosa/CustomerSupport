using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IAttachmentService
    {
        Task<bool> AddAttachment(TicketAttachmentDto attachment);
        Task<bool> AddAttachment(List<TicketAttachmentDto> attachments);
    }
}
