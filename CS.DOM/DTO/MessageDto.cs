namespace CS.DOM.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public List<Guid> AttachmentIds { get; set; }
        public DateTime? WhenSended { get; set; }
    }
}