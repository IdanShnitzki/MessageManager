using AutoMapper;
using MessageManagerService.Controllers.Models;
using MessageManagerService.Dtos;

namespace MessageManagerService.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageReadDto>();
            CreateMap<MessageCreateDto, Message>();
            CreateMap<MessageUpdateDto, Message>();
            CreateMap<Message, MessageUpdateDto>();
            CreateMap<Message, MessageCreateDto>();
        }
    }
}
