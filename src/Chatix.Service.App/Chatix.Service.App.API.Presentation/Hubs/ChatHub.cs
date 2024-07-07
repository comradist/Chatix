using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.User.Handlers.Commands;
using Chatix.Service.App.Domain.Features.User.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Chatix.Service.App.API.Presentation.Hubs;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateUser(string createUserDto)
    {
        var user = await _mediator.Send(new CreateUserCommand { UserDto = new CreateUserDto { LastName = createUserDto } });

        await Clients.Caller.SendAsync("ReceiveUser", user);
    }

    // public async Task SendMessage(MessageDto sendMessageDto)
    // {
    //     var message = await _mediator.Send(new  { SendMessageDto = sendMessageDto });

    //     await Clients.Group(sendMessageDto.RoomId.ToString()).SendAsync("ReceiveMessage", message);
    // }

    // public async Task JoinRoom(Guid roomId)
    // {
    //     await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

    //     var messages = await _mediator.Send(new GetMessagesByRoomIdQuery { RoomId = roomId });

    //     await Clients.Caller.SendAsync("ReceiveMessages", messages);
    // }

    public async Task LeaveRoom(Guid roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
    }
}