using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ITicketService
    {
        Task<List<TicketShortInfoDto>> GetAll();
        Task<List<TicketShortInfoDto>> GetAllUnAsssigned();
        Task<TicketFullInfoDto?> GetById(Guid ticketId);
        Task<bool> Create(TicketCreateDto ticketDto);
        Task<bool> Update(TicketUpdateDto ticketDto);
        Task<bool> Delete(Guid ticketId);
    }
}
