using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IMessageService
    {
        Task<DTOMessage?> GetById(Guid id);
        Task<bool> Create(Message message);
        Task<bool> Update(Message message);
        Task<bool> Delete(Guid id);
    }
}
