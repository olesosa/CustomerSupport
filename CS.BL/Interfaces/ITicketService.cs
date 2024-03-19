using CS.BL.Helpers;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Pagination;

namespace CS.BL.Interfaces
{
    public interface ITicketService
    {
        Task<PagedResponse<List<TicketShortInfoDto>>> GetAll(PaginationFilter filter, CancellationToken cancellationToken = default);
        Task<TicketFullInfoDto?> GetFullInfoById(Guid ticketId, CancellationToken cancellationToken = default);
        Task<TicketShortInfoDto> Create(TicketCreateDto ticketDto, Guid userId);
        Task<TicketShortInfoDto> AssignTicket(Guid ticketId, Guid adminId, CancellationToken cancellationToken = default);
        Task<TicketShortInfoDto> UnAssignTicket(Guid ticketId, CancellationToken cancellationToken = default);
        Task<bool> IsTicketExist(Guid ticketId);
        
    }
}
