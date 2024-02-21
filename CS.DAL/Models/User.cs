namespace CS.DAL.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsDeleted { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<Ticket> AssignedTickets { get; set; }
        public List<Message> Messages { get; set; }

    }
}
