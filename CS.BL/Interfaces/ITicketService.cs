using CS.BL.Helpers;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketShortInfoDto>> GetAll(TicketFilter filter, CancellationToken cancellationToken = default);
        Task<TicketFullInfoDto?> GetFullInfoById(Guid ticketId, CancellationToken cancellationToken = default);
        Task<TicketShortInfoDto> Create(TicketCreateDto ticketDto, Guid userId);
        Task<bool> Delete(Guid ticketId);
        Task<bool> AssignTicket(Guid ticketId, Guid adminId);
        Task<bool> UnAssignTicket(Guid ticketId);
        Task<bool> IsTicketExist(Guid ticketId);
        
    }
}
