namespace CS.DAL.Models
{
    public class TicketDetails : BaseEntity // TODO redo using epoch time
    {
        public Ticket Ticket { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsSolved { get; set; }
        public bool IsClosed { get; set; }
    }
}
