namespace CS.DAL.Models
{
    public class TicketDetails : BaseDetails
    {
        public Ticket Ticket { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}
