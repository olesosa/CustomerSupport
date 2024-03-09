using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IDialogService
    {
        Task<DialogDto?> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<DialogCreateDto> Create(Guid ticketId, Guid adminId);
    }
}
