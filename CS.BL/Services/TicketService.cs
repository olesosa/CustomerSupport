using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class TicketService : BaseService<Ticket>, ITicketService
    {
        public TicketService(ApplicationContext context) : base(context) { }

        public async Task<List<DTOTicketShortInfo>> GetAll()
        {
            return await _context.Tickets
                .Select(t =>
                new DTOTicketShortInfo()
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    RequestType = t.RequestType,
                    IsAssigned = t.IsAssigned,
                    Topic = t.Details.Topic,
                }).ToListAsync(); // rewrite with inclue
        }

        public async Task<DTOTicketFullInfo> GetById(Guid id)
        {
            _context.Tickets.Select(t => new DTOTicketFullInfo()
            {
                Id = t.Id,
                CustomerId = t.CustomerId,
                RequestType = t.RequestType,
                IsAssigned = t.IsAssigned,
                Topic = t.Details.Topic,
                Description = t.Details.Description,
                AttachmentsFilePath = t.Attachments.Select(a => a.FilePath).ToList()
            }); // rewrite with inclue

            return null;
        }
    }
}
