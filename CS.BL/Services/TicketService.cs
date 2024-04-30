using AutoMapper;
using CS.BL.Extensions;
using CS.BL.Interfaces;
using CS.DAL.DataAccess;
using CS.DAL.Models;
using CS.DOM.DTO;
using CS.DOM.Helpers;
using CS.DOM.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Services;

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
        var tickets = await _context.Tickets
                                    .Include(t => t.Details)
                                    .AsQueryable()
                                    .Paginate(filter, cancellationToken);

        var ticketDtos = tickets
                         .Select(t => _mapper.Map<TicketShortInfoDto>(t))
                         .ToList();

        var totalRecords = await _context.Tickets.CountAsync(cancellationToken);
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

    public async Task<TicketFullInfoDto> GetFullInfo(int number, CancellationToken cancellationToken = default)
    {
        var ticket = await _context.Tickets
                                   .Include(t => t.Details)
                                   .Include(t => t.Attachments)
                                   .Include(t => t.Dialog)
                                   .FirstOrDefaultAsync(t => t.Number == number, cancellationToken);

        if (ticket == null)
        {
            throw new ApiException(404, "Ticket not found");
        }

        return _customMapper.MapToTicketFullInfo(ticket);
    }
        
    public async Task<List<StatisticDto>> GetTicketsStatistic(StatisticFilter filter)
    {
        var query = _context.Tickets.AsQueryable();

        if (filter.UserId.HasValue)
        {
            query = query.Where(t => t.CustomerId == filter.UserId);
        }

        if (filter.RequestType.HasValue)
        {
            query = query.Where(t => t.RequestType == filter.RequestType);
        }

        if (filter.IsAssigned.HasValue)
        {
            query = query.Where(t => t.Details.IsAssigned == filter.IsAssigned);
        }

        if (filter.IsSolved.HasValue)
        {
            query = query.Where(t => t.Details.IsSolved == filter.IsSolved);
        }

        if (filter.IsClosed.HasValue)
        {
            query = query.Where(t => t.Details.IsClosed == filter.IsClosed);
        }

        var statistics = await query
                               .GroupBy(t => t.RequestType)
                               .Select(g => new StatisticDto
                               {
                                   RequestType = g.Key,
                                   Count = g.Count()
                               })
                               .ToListAsync();

        return statistics;
    }
}