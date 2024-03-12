﻿namespace CS.DAL.Models
{
    public class Ticket : BaseEntity // TODO redo using epoch time
    {
        public Guid CustomerId { get; set; }
        public User Customer { get; set; }
        public Guid? AdminId { get; set; }
        public User? Admin { get; set; }
        public string RequestType { get; set; }
        public string Topic { get; set; }
        public bool IsAssigned { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public Guid DetailsId { get; set; }
        public TicketDetails Details { get; set; }
        public List<TicketAttachment> Attachments { get; set; }
        public Dialog Dialog { get; set; }
    }
}
