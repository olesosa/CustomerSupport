using CS.DOM.Enums;

namespace CS.DOM.DTO
{
    public class TicketCreateDto
    {
        public RequestTypes RequestType { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
    }
}
