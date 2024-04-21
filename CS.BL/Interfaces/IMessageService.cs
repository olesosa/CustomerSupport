using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetAll(Guid dialogId, CancellationToken cancellationToken);
        Task SaveMessage(ChatMessageDto messageDto, Guid userId);
        Task MarkAsRead(Guid dialogId);
    }
}
