namespace CustomerSupportData.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public bool IsDeleted { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
