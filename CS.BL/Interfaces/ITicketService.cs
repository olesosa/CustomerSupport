using CS.API.Filters;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketShortInfoDto>> GetAll(TicketFilter filter, CancellationToken cancellationToken = default);
        Task<TicketFullInfoDto?> GetById(Guid ticketId, CancellationToken cancellationToken = default);
        Task<bool> Create(TicketCreateDto ticketDto);
        Task<bool> Update(TicketUpdateDto ticketDto);
        Task<bool> Delete(Guid ticketId, CancellationToken cancellationToken = default);
        Task<bool> AssignTicket(Guid ticketId, Guid adminId, CancellationToken cancellationToken = default);
        Task<bool> UnAssignTicket(Guid ticketId, CancellationToken cancellationToken = default);
    }
}
