using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class TicketService : BaseService, ITicketService
    {
        public TicketService(ApplicationContext context) : base(context) { }

        public async Task<bool> Create(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);

            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            _context.Tickets.Remove(await _context.Tickets.FirstOrDefaultAsync(u => u.Id == id));

            return await SaveAsync();
        }

        public async Task<DTOTicketFullInfo?> GetById(Guid id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Details)
                .Include(t => t.Attachments)
                .FirstOrDefaultAsync(t => t.Id == id);

            return new DTOTicketFullInfo()
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                RequestType = ticket.RequestType,
                IsAssigned = ticket.IsAssigned,
                Topic = ticket.Details.Topic,
                Description = ticket.Details.Description,
                WhenCreated = ticket.Details.WhenCreated,
                AttachmentsFilePath = ticket.Attachments
                .Select(t => t.FilePath).ToList(),
            };
        }

        public async Task<bool> Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);

            return await SaveAsync();
        }
        public async Task<List<DTOTicketShortInfo>> GetAll()
        {
            return await _context.Tickets
                .Include(t => t.Details)
                .Select(t =>
                new DTOTicketShortInfo()
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    RequestType = t.RequestType,
                    IsAssigned = t.IsAssigned,
                    Topic = t.Details.Topic,
                })
                .ToListAsync();
        }
        public async Task<List<DTOTicketShortInfo>> GetAllUnAsssigned()
        {
            return await _context.Tickets
                .Include(t => t.Details)
                .Where(t => t.IsAssigned == false)
                .Select(t =>
                new DTOTicketShortInfo()
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    RequestType = t.RequestType,
                    IsAssigned = t.IsAssigned,
                    Topic = t.Details.Topic,
                })
                .ToListAsync();
        }
    }
}
