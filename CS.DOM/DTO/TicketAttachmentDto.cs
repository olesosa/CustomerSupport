namespace CS.DOM.DTO
{
    public class TicketAttachmentDto
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }
        public Guid TicketId { get; set; }
    }
}
