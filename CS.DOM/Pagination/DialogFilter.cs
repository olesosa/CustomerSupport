namespace CS.DOM.Pagination;

public class DialogFilter
{
    public Guid? UserId { get; set; }
    public string? RoleName { get; set; }
    public string SortDir { get; set; } = "asc";//can be boolean
    public bool? DateTime { get; set; }//unclear what it does
}   