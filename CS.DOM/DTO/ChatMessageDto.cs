namespace CS.DOM.DTO
{
    public class ChatMessageDto
    {
        public Guid DialogId { get; set; }
        public Guid ReceiverId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime WhenSend { get; set; }
    }
}
