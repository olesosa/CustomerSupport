using AutoMapper;
using CS.DAL.Models;
using CS.DOM.DTO;

namespace CS.BL.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<TicketShortInfoDto, Ticket>();
            CreateMap<Ticket, TicketShortInfoDto>();

            CreateMap<Ticket, TicketCreateDto>();
            CreateMap<TicketCreateDto, Ticket>();
            
            CreateMap<User, UserInfoDto>();
            CreateMap<UserInfoDto, User>();

            CreateMap<ChatMessageDto, Message>();
            CreateMap<Message, ChatMessageDto>();

            CreateMap<TicketShortInfoDto, TicketCreateDto>();
            CreateMap<TicketCreateDto, TicketShortInfoDto>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
