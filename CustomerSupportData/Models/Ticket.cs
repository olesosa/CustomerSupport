﻿namespace CustomerSupportData.Models
{
    public class Ticket : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public Guid AdminId { get; set; }
        public User Admin { get; set; }
        public string Subject { get; set; }
        public Guid DetailsId { get; set; }
        public TicketDetails Details { get; set; }
        public List<TicketAttachment> Attachments { get; set; }
        public Dialog Dialog { get; set; }
    }
}
