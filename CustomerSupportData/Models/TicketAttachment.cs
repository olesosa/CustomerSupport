namespace CustomerSupportData.Models
{
    public class TicketAttachment : BaseAttachment
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
