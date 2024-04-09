namespace CS.DOM.Pagination;

public class  PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    // Filter
    public string? RequestType { get; set; }
    public bool? IsAssigned { get; set; }
    public bool? IsSolved { get; set; }
    public bool? IsClosed { get; set; }
    public Guid? UserId { get; set; }
    // Sorting
    public string SortDir { get; set; } = "asc";
    public bool? Number { get; set; }
}