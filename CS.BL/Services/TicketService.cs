using AutoMapper;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using CS.DOM.Pagination;
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

        public async Task<TicketShortInfoDto> Create(TicketCreateDto ticketDto, Guid userId)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);

            ticket.CustomerId = userId;

            await _context.Tickets.AddAsync(ticket);

            var details = new TicketDetails()
            {
                Description = ticketDto.Description,
                CreationTime = DateTime.Now,
                IsAssigned = false,
                IsSolved = false,
                IsClosed = false,
                TicketId = ticket.Id,
            };

            await _context.TicketDetails.AddAsync(details);

            await _context.SaveChangesAsync();

            return _mapper.Map<TicketShortInfoDto>(ticket);
        }

        public async Task<PagedResponse<List<TicketShortInfoDto>>> GetAll(TicketFilter filter,
            CancellationToken cancellationToken = default)
        {
            var tickets = _context.Tickets
                .Include(t => t.Details)
                .AsQueryable();
            
            if (filter.RequestType != null)
            {
                tickets = tickets.Where(t => t.RequestType == filter.RequestType);
            }

            if (filter.IsAssigned.HasValue)
            {
                tickets = tickets.Where(t => t.Details.IsAssigned == filter.IsAssigned);
            }

            if (filter.IsSolved.HasValue)
            {
                tickets = tickets.Where(t => t.Details.IsSolved == filter.IsSolved);
            }

            if (filter.IsClosed.HasValue)
            {
                tickets = tickets.Where(t => t.Details.IsClosed == filter.IsClosed);
            }

            if (filter.UserId.HasValue)
            {
                tickets = tickets.Where(t => t.CustomerId == filter.UserId);
            }

            if (filter.SortDir == "asc")
            {
                if (filter.Number.HasValue)
                {
                    tickets = tickets.OrderBy(t => t.Number);
                }
            }
            else
            {
                if (filter.Number.HasValue)
                {
                    tickets = tickets.OrderByDescending(t => t.Number);
                }
            }

            var ticketsList = await tickets
                .Skip(filter.Skip)
                .Take(filter.Take)
                .ToListAsync(cancellationToken);

            var ticketDtos = ticketsList
                .Select(t => _mapper.Map<TicketShortInfoDto>(t))
                .ToList();

            var totalRecords = await _context.Tickets.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)filter.Take);

            if (ticketDtos == null)
            {
                throw new ApiException(404, "Tickets not found");
            }

            var pagedResponse = new PagedResponse<List<TicketShortInfoDto>>
            {
                Data = ticketDtos,
                PageNumber = filter.Skip,
                PageSize = filter.Take,
                TotalRecords = totalRecords,
                TotalPages = totalPages
            };

            return pagedResponse;
        }

        public async Task<TicketFullInfoDto> GetFullInfoById(Guid id, CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Details)
                .Include(t => t.Attachments)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (ticket == null)
            {
                throw new ApiException(404, "Ticket not found");
            }

            return _customMapper.MapToTicketFullInfo(ticket);
        }

        public async Task<TicketShortInfoDto> AssignTicket(Guid ticketId, Guid adminId,
            CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Details)
                .FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);

            if (ticket == null)
            {
                throw new ApiException(404, "Ticket not found");
            }

            ticket.AdminId = adminId;
            ticket.Details.IsAssigned = true;
            ticket.Details.AssignmentTime = DateTime.Now;

            _context.Tickets.Update(ticket);

            return _mapper.Map<TicketShortInfoDto>(ticket);
        }

        public async Task<TicketShortInfoDto> UnAssignTicket(Guid ticketId,
            CancellationToken cancellationToken = default)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Details)
                .FirstOrDefaultAsync(t => t.Id == ticketId, cancellationToken);

            if (ticket == null)
            {
                throw new ApiException(404, "Ticket not found");
            }

            ticket.AdminId = null;
            ticket.Details.IsAssigned = false;
            ticket.Details.AssignmentTime = null;

            return _mapper.Map<TicketShortInfoDto>(ticket);
        }

        public async Task<bool> IsTicketExist(Guid ticketId)
        {
            var result = await _context.Tickets.AnyAsync(t => t.Id == ticketId);

            return result;
        }
    }
}