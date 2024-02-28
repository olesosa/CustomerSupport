namespace CS.DOM.DTO
{
    public class TicketUpdateDto
    {
        public Guid Id { get; set; }
        public bool IsSolved { get; set; }
        public bool IsAssigned { get; set; }
        public string RequestType { get; set; }
    }
}
