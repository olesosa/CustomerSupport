using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface ITicketService : IBaseService<Ticket>
    {
        Task<List<DTOTicketShortInfo>> GetAll();
        new Task<DTOTicketFullInfo> GetById(Guid id);
    }
}
