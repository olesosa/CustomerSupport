using CS.DOM.DTO;
using CS.DOM.Pagination;

namespace CS.BL.Interfaces
{
    public interface ITicketService
    {
        Task<PagedResponse<List<TicketShortInfoDto>>> GetAll(TicketFilter filter, CancellationToken cancellationToken = default);
        Task<TicketFullInfoDto> GetFullInfo(int number, CancellationToken cancellationToken = default);
        Task<TicketShortInfoDto> Create(TicketCreateDto ticketDto, Guid userId);
        Task<List<StatisticDto>> GetTicketsStatistic(StatisticFilter filter);
    }
}
