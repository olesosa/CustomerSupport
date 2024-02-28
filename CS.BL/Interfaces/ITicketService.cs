using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ITicketService
    {
        Task<List<DTOTicketShortInfo>> GetAll();
        Task<List<DTOTicketShortInfo>> GetAllUnAsssigned();
        Task<DTOTicketFullInfo> GetById(Guid id);
        Task<bool> Create(Ticket ticket);
        Task<bool> Update(Ticket ticket);
        Task<bool> Delete(Guid id);
    }
}
