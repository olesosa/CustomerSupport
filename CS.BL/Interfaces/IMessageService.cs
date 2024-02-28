using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IMessageService
    {
        Task<MessageDto?> GetById(Guid id);
        Task<bool> Create(Message message);
        Task<bool> Update(Message message);
        Task<bool> Delete(Guid id);
        Task<List<MessageDto>?> GetAllByDialogId(Guid dialogId);
    }
}
