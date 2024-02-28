using AutoMapper;
using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace CS.BL.Services
{
    public class TicketService : ITicketService
    {
        readonly ApplicationContext _context;
        readonly IMapper _mapper;
        readonly ICustomMapper _customMapper;
        public TicketService(ApplicationContext context, IMapper mapper, ICustomMapper customMapper)
        {
            _context = context;
            _mapper = mapper;
            _customMapper = customMapper;
        }
        private async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0 ? true : false;
        }

        public async Task<bool> Create(TicketCreateDto ticketDto)
        {
            var ticket = _customMapper.MapToTicket(ticketDto);

            await _context.AddAsync(ticket);
                
            return await SaveAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(u => u.Id == id);

            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            return await SaveAsync();
        }

        public async Task<List<TicketShortInfoDto>> GetAll()
        {
            return await _context.Tickets
                .Include(t => t.Details)
                .Select(t => _customMapper.MapToTicketShortInfo(t))
                .ToListAsync();
        }

        public async Task<List<TicketShortInfoDto>> GetAllUnAsssigned()
        {
            return await _context.Tickets
                .Include(t => t.Details)
                .Where(t => !t.IsAssigned)
                .Select(t => _customMapper.MapToTicketShortInfo(t))
                .ToListAsync();
        }

        public async Task<TicketFullInfoDto?> GetById(Guid id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Details)
                .Include(t => t.Attachments)
                .FirstOrDefaultAsync(t => t.Id == id);

            return _customMapper.MapToTicketFullInfo(ticket);
        }

        public async Task<bool> Update(TicketUpdateDto ticketDto)
        {
            var ticket = _customMapper.MapUpdateTicket(ticketDto);

            _context.Tickets.Update(ticket);

            return await SaveAsync();
        }

    }
}
