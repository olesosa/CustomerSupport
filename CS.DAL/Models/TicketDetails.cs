namespace CS.DAL.Models
{
    public class TicketDetails : BaseEntity
    {
        public Guid TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsAssigned { get; set; }
        public DateTime? AssignmentTime { get; set; }
        public bool IsSolved { get; set; }
        public bool IsClosed { get; set; }
    }
}
