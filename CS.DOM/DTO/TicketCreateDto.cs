namespace CS.DOM.DTO
{
    public class TicketCreateDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string RequestType { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime? WhenCreated { get; set; }
    }
}
