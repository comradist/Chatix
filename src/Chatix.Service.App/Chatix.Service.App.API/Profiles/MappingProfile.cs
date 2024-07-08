using AutoMapper;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Service.App.API.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Message, MessageDto>().ReverseMap();
        CreateMap<Message, CreateMessageDto>().ReverseMap();
        CreateMap<Message, UpdateMessageDto>().ReverseMap();

        CreateMap<Room, RoomDto>().ReverseMap();
        CreateMap<Room, CreateRoomDto>().ReverseMap();
        CreateMap<Room, UpdateRoomDto>().ReverseMap();

        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, CreateUserDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
    }
}