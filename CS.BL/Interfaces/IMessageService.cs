using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto?> GetById(Guid id, CancellationToken cancellationToken);
        Task<List<MessageDto>?> GetAllByDialogId(Guid dialogId, CancellationToken cancellationToken);
        Task<bool> SendMessage(SendMessageDto messageDto);
    }
}
