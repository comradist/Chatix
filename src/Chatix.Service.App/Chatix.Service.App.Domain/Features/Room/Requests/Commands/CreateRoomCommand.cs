using Chatix.Libs.Core.Shared.DTOs.Room;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Room.Requests.Commands;

public class CreateRoomCommand : IRequest<RoomDto>
{
    public CreateRoomDto RoomDto { get; set; }
}