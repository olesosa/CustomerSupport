using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetAll(Guid dialogId, CancellationToken cancellationToken);
        Task<Message> SaveMessage(string text, Guid dialogId, Guid userId);
        Task MarkAsRead(Guid dialogId);
    }
}
