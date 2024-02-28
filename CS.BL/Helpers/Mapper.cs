using AutoMapper;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Helpers
{
    public class Mapper : Profile
    {
        public Mapper() 
        {
            CreateMap<Ticket, TicketFullInfoDto>();
            CreateMap<TicketFullInfoDto, Ticket>();

            CreateMap<Ticket, TicketShortInfoDto>();
            CreateMap<TicketShortInfoDto, Ticket>();

            CreateMap<TicketAttachment, TicketFullInfoDto>();
            CreateMap<TicketFullInfoDto, TicketAttachment>();

            CreateMap<TicketDetails, TicketShortInfoDto>();
            CreateMap<TicketShortInfoDto, TicketDetails>();

            CreateMap<Dialog, DialogDto>();
            CreateMap<DialogDto, Dialog>();

            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>();

        }
    }
}
