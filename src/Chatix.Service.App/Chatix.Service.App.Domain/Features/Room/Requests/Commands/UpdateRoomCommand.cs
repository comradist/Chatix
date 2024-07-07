using Chatix.Libs.Core.Shared.DTOs.Room;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Room.Requests.Commands;

public class UpdateRoomCommand : IRequest<Unit>
{
    public UpdateRoomDto UpdateRoomDto { get; set; }
}