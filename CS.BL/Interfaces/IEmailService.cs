using CS.DAL.Models;

namespace CS.BL.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Message message);
        Task<bool> SendEmail(Ticket ticket);
    }
}
