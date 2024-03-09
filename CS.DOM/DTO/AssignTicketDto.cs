using System.ComponentModel.DataAnnotations;

namespace CS.DOM.DTO
{
    public class AssignTicketDto
    {
        public Guid ticketId { get; set; }
        public Guid adminId { get; set; }
    }
}
