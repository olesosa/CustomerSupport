using CS.DOM.Enums;

namespace CS.DOM.DTO
{
    public class TicketFullInfoDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Number { get; set; }
        public RequestTypes RequestType { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public List<Guid> AttachmentIds { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsSolved { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}
