namespace CS.DOM.DTO
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string MessageText { get; set; }
        public bool IsRead { get; set; }
        public List<string> FilePath { get; set; }
        public DateTime? WhenSended { get; set; }
    }
}