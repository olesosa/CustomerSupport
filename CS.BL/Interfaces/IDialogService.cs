using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IDialogService
    {
        Task<DialogDto?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<bool> Create(DialogCreateDto dialogDto);
        Task<bool> Update(Dialog dialog);
        Task<bool> Delete(Guid id, CancellationToken cancellationToken = default);
    }
}
