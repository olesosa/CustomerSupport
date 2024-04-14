namespace CS.DAL.Models
{
    public class Ticket : BaseEntity
    {
        public int Number { get; set; }
        public string RequestType { get; set; }
        public string Topic { get; set; }
        
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public Guid? AdminId { get; set; }
        public User? Admin { get; set; }
        public TicketDetails Details { get; set; }
        public List<TicketAttachment> Attachments { get; set; }
        public Dialog Dialog { get; set; }
    }
}
