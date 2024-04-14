namespace CS.DOM.Pagination;

public class  TicketFilter
{
    public int Skip { get; set; }
    public int Take { get; set; }

    public string? RequestType { get; set; }
    public bool? IsAssigned { get; set; }
    public bool? IsSolved { get; set; }
    public bool? IsClosed { get; set; }
    public Guid? UserId { get; set; }

    public string SortDir { get; set; } = "asc";
    public bool? Number { get; set; }
}