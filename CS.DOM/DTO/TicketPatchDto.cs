namespace CS.DOM.DTO;

public class TicketPatchDto
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public bool IsAssigned { get; set; }
    public bool IsSolved { get; set; }
    public bool IsClosed { get; set; }
    public bool? HasReceived { get; set; }
}