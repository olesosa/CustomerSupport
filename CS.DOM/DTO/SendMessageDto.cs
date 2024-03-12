using System.ComponentModel.DataAnnotations;

namespace CS.DOM.DTO
{
    public class SendMessageDto
    {
        public Guid Id { get; set; }
        public Guid DialogId { get; set; }
        public string MessageText { get; set; }
    }
}
