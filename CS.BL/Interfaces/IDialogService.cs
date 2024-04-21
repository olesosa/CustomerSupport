using CS.DOM.DTO;
using CS.DOM.Pagination;

namespace CS.BL.Interfaces
{
    public interface IDialogService
    {
        Task<DialogDto> GetById(Guid id, Guid userId, CancellationToken cancellationToken = default);
        Task<List<DialogShortInfoDto>> GetAllDialogs(DialogFilter filter, CancellationToken cancellationToken);
        Task<DialogCreateDto> Create(Guid ticketId, Guid adminId);
    }
}
