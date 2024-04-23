namespace CS.DOM.DTO
{
    public class MessageDto
    {
        public Guid DialogId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public List<Guid> Attachments { get; set; }
        public DateTime? WhenSended { get; set; }
    }
}