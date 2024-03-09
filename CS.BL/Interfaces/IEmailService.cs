using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Message message);
        Task<bool> SendEmail(Ticket ticket);
    }
}
