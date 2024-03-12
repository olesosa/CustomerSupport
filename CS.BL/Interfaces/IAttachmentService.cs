using CS.DOM.DTO;
using Microsoft.AspNetCore.Http;

namespace CS.BL.Interfaces
{
    public interface IAttachmentService
    {
        Task<string> AddAttachment(IFormFile file, Guid guid);
    }
}
