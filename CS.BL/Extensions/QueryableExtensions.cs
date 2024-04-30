using CS.DAL.Models;
using CS.DOM.Helpers;
using CS.DOM.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CS.BL.Extensions;

public static class QueryableExtensions
{
    public static async Task<IEnumerable<Ticket>> Paginate(this IQueryable<Ticket> query,
        TicketFilter filter, CancellationToken cancellationToken)//can be revised using params Expression<Func<T, object>>[] filters. it will remove all if statements
    {
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

        if (filter.UserId.HasValue)
        {
            query = query.Where(t => t.CustomerId == filter.UserId);
        }

        if (filter.SortDir == "asc")
        {
            if (filter.Number.HasValue)
            {
                query = query.OrderBy(t => t.Number);
            }
        }
        else
        {
            if (filter.Number.HasValue)
            {
                query = query.OrderByDescending(t => t.Number);
            }
        }

        return await query
            .Skip(filter.Skip)
            .Take(filter.Take)
            .ToListAsync(cancellationToken);
    }

    public static async Task<IEnumerable<Dialog>> PaginateDialogs(this IQueryable<Dialog> query,
        DialogFilter filter,
        CancellationToken cancellationToken)
    {
        query = filter.RoleName switch
        {
            "User" => query.Where(d => d.Ticket.CustomerId == filter.UserId),
            "Admin" => query.Where(d => d.Ticket.AdminId == filter.UserId),
            _ => throw new ApiException(400, "Invalid role name")
        };

        if (filter.SortDir == "asc")//lines 70-85 can be refactored and simplified
        {
            if (filter.DateTime.HasValue)
            {
                query = query.OrderByDescending(d => d.Messages.OrderBy(m => m.WhenSend).LastOrDefault());
            }
        }
        else
        {
            if (filter.DateTime.HasValue)
            {
                query = query.OrderBy(d => d.Messages.OrderBy(m => m.WhenSend).LastOrDefault());
            }
        }

        return await query.ToListAsync(cancellationToken);
    }
}