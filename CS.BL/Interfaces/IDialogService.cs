using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IDialogService
    {
        Task<DialogDto?> GetById(Guid id);
        Task<bool> Create(Dialog dialog);
        Task<bool> Update(Dialog dialog);
        Task<bool> Delete(Guid id);
    }
}
