using Chatix.Libs.Core.Shared.DTOs.Room;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Rooms.Requests.Commands;

public class DeleteRoomCommand : IRequest<RoomDto>
{
    public DeleteRoomDto deleteRoomDto { get; set; }

}