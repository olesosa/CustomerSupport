using CS.DOM.Enums;

namespace CS.DOM.DTO;

public class DialogTicketDto
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid AdminId { get; set; }
    public int Number { get; set; }
    public RequestTypes Request { get; set; }
    public string Topic { get; set; }
}