namespace CS.DOM.DTO
{
    public class DialogDto
    {
        public Guid Id { get; set; }
        public List<MessageDto> Messages { get; set; }
        public DialogTicketDto Ticket { get; set;  }
    }
}
