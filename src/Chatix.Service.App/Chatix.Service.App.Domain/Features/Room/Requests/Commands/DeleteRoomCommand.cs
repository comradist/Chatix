using Chatix.Libs.Core.Shared.DTOs.Room;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Room.Requests.Commands;

public class DeleteRoomCommand : IRequest<Unit>
{
    public DeleteRoomDto deleteRoomDto { get; set; }

}