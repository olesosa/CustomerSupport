﻿using AutoMapper;
using CS.BL.Helpers;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ICustomMapper _customMapper;
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
        
        private async Task<Ticket?> GetById(Guid ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);

            return ticket;
        }

        public async Task<TicketShortInfoDto> Create(TicketCreateDto ticketDto, Guid userId)
        {
            var ticket = _customMapper.MapToTicket(ticketDto);

            ticket.CustomerId = userId;
            
            await _context.AddAsync(ticket);

            await _context.SaveChangesAsync();

            var createdTicket = await GetById(ticket.Id);

            return _mapper.Map<TicketShortInfoDto>(createdTicket);
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

        public async Task<List<TicketShortInfoDto>> GetAll(TicketFilter filter, CancellationToken cancellationToken = default)
        {
            return await _context.Tickets
                .Include(t => t.Details)
                .Where(t => t.IsAssigned == filter.IsAssigned)
                .Select(t => _mapper.Map<TicketShortInfoDto>(t))
                .ToListAsync(cancellationToken);
        }

        public async Task<TicketFullInfoDto?> GetFullInfoById(Guid id, CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Details)
                .Include(t => t.Attachments)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            return _customMapper.MapToTicketFullInfo(ticket);
        }

        public async Task<bool> AssignTicket(Guid ticketId, Guid adminId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket != null)
            {
                ticket.AdminId = adminId;
                ticket.IsAssigned = true;
                ticket.AssignmentTime = DateTime.Now;
            }

            return await SaveAsync();
        }

        public async Task<bool> UnAssignTicket(Guid ticketId)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (ticket != null)
            {
                ticket.AdminId = null;
                ticket.IsAssigned = false;
                ticket.AssignmentTime = null;
            }

            return await SaveAsync();
        }

        public async Task<bool> IsTicketExist(Guid ticketId)
        {
            var result = await _context.Tickets.AnyAsync(t => t.Id == ticketId);

            return result;
        }



    }
}
