namespace CustomerSupportData.Models
{
    public class MessageAttachment : BaseAttachment
    {
        public Guid MessageId { get; set; }
        public Message Message { get; set; }
    }
}
