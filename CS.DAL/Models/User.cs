namespace CS.DAL.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Ticket> AssignedTickets { get; set; }
        public List<Message> Messages { get; set; }

    }
}
