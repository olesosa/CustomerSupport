using AutoMapper;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Helpers
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<TicketAttachment, TicketAttachmentDto>();
            CreateMap<TicketAttachmentDto, TicketAttachment>();

            CreateMap<Ticket, TicketUpdateDto>();
            CreateMap<TicketUpdateDto, Ticket>();

        }
    }
}
