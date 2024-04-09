namespace CS.DOM.DTO
{
    public class DialogDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? AdminId { get; set; }
        public List<MessageDto> Messages { get; set; }
        public TicketFullInfoDto Ticket { get; set;  }
    }
}
