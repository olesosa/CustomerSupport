namespace CS.DOM.DTO;

public class DetailsPatchDto
{
    public Guid TicketId { get; set; }
    public bool IsSolved { get; set; }
    public bool IsClosed { get; set; }
}