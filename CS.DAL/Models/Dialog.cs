namespace CS.DAL.Models
{
    public class Dialog : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public List<Message> Messages { get; set; }
    }
}
