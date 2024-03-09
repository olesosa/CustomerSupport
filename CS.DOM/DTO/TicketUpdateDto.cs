using System.ComponentModel.DataAnnotations;

namespace CS.DOM.DTO
{
    public class TicketUpdateDto
    {
        [Required]
        public Guid Id { get; set; }
        public bool IsSolved { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsClosed { get; set; }
        public string RequestType { get; set; }
    }
}
