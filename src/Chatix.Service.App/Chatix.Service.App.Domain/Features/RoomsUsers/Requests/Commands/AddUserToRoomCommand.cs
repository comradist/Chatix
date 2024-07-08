using Chatix.Libs.Core.Models.Entities;
using MediatR;

namespace Chatix.Service.App.Domain.Features.RoomsUsers.Requests.Commands;

public class AddUserToRoomCommand : IRequest<Unit>
{
    public RoomUser RoomUser { get; set; }
}