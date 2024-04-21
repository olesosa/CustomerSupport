using CS.DOM.Enums;

namespace CS.DOM.DTO
{
    public class TicketShortInfoDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int Number { get; set; }
        public RequestTypes RequestType { get; set; }
        public string Topic { get; set; }
    }
}
