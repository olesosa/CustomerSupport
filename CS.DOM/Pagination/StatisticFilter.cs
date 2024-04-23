using CS.DOM.Enums;

namespace CS.DOM.Pagination;

public class StatisticFilter
{
    public RequestTypes? RequestType { get; set; }
    public bool? IsAssigned { get; set; }
    public bool? IsSolved { get; set; }
    public bool? IsClosed { get; set; }
    public Guid? UserId { get; set; }
 }