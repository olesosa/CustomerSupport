namespace CS.DAL.Models
{
    public class TicketDetails : BaseEntity
    {
        public Ticket Ticket { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? AssignmentTime { get; set; }
    }
}
