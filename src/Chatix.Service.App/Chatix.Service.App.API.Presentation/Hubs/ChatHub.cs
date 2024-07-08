using AutoMapper;
using Chatix.Libs.Core.Contracts.Logger;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Queries;
using Chatix.Service.App.Domain.Features.Users.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Chatix.Service.App.API.Presentation.Hubs;

public class ChatHub : Hub
{
    private readonly IMediator mediator;
    private readonly ILoggerManager logger;
    private readonly IMapper mapper;
    private static readonly Dictionary<string, (Guid UserId, Guid RoomId)> Connections = new();

    public ChatHub(IMediator mediator, ILoggerManager logger, IMapper mapper)
    {
        this.mediator = mediator;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task SendPrivateMessage(Guid userId, string message)
    {
        var connection = Connections.FirstOrDefault(c => c.Value.UserId == userId);
        if (connection.Key != null)
        {
            await Clients.Client(connection.Key).SendAsync("ReceivePrivateMessage", message);
        }
        else
        {
            await Clients.Caller.SendAsync("onError", "User not found or not connected");
        }
    }

    public async Task Join(RoomUser roomUser)
    {
        //! Its need to be refactored
        var user = await mediator.Send(new GetUserByIdRequest { Id = roomUser.UserId }) ?? throw new Exception("User not found");
        var room = await mediator.Send(new GetRoomByIdRequest { Id = roomUser.RoomId }) ?? throw new Exception("Room not found");

        // Add user connection mapping
        Connections.Add(Context.ConnectionId, (user.Id, room.Id));

        await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());
        await Clients.Group(room.Id.ToString()).SendAsync("addUser", user.Id ,$"{user.FirstName + " " + user.LastName} joined the chat room!");

    }

    public async Task CloseRoomsByAdmin(Guid adminId)
    {
        //! Its need to be refactored
        var user = await mediator.Send(new GetUserByIdRequest { Id = adminId }) ?? throw new Exception("User not found");
        if (user.CreatedRooms == null)
        {
            await Clients.Caller.SendAsync("onError", "User did not create any rooms!");
            return;
        }
        foreach (var room in user.CreatedRooms)
        {
            await CloseRoom(new RoomUser { RoomId = room.Id, UserId = adminId });
        }
    }

    public async Task CloseRoom(RoomUser roomUser)
    {
        //! Its need to be refactored
        var room = await mediator.Send(new GetRoomByIdRequest { Id = roomUser.RoomId }) ?? throw new Exception("Room not found");
        var user = await mediator.Send(new GetUserByIdRequest { Id = roomUser.UserId }) ?? throw new Exception("User not found");
    
        if(room.Admin.Id != roomUser.UserId)
        {
            await Clients.Caller.SendAsync("onError", "You are not the owner of the room!");
            return;
        }
        
        var connectionsToRemove = Connections.Where(c => c.Value.RoomId == room.Id).ToList();


        // Notify all users in the group that the room is closed
        await Clients.Group(room.Id.ToString()).SendAsync("groupClosed", $"The room {room.Name} has been closed.");

        foreach (var connection in connectionsToRemove)
        {
            // Remove the user from the group
            await Groups.RemoveFromGroupAsync(connection.Key, room.Id.ToString());
        }


        // Remove all the connections mapping for this room
        foreach (var connection in connectionsToRemove)
        {
            Connections.Remove(connection.Key);
        }
    }

    public async Task LeaveRoom(RoomUser roomUser)
    {
        //! Its need to be refactored
        var room = await mediator.Send(new GetRoomByIdRequest { Id = roomUser.RoomId }) ?? throw new Exception("Room not found");
        var user = await mediator.Send(new GetUserByIdRequest { Id = roomUser.UserId }) ?? throw new Exception("User not found");

        if (room.Admin.Id == roomUser.UserId)
        {
            await CloseRoom(roomUser);
        }

        if (Connections.TryGetValue(Context.ConnectionId, out var userRoom))
        {
            if (userRoom.UserId == user.Id)
            {
                if (room != null)
                {
                    // Remove the user from the group
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Id.ToString());
                    await Clients.Group(room.Id.ToString()).SendAsync("removeUser", userRoom.UserId);

                    // Remove the connection mapping
                    //Connections.Remove(Context.ConnectionId);
                }
            }
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (Connections.TryGetValue(Context.ConnectionId, out var userRoom))
        {
            var room = await mediator.Send(new GetRoomByIdRequest { Id = userRoom.RoomId });
            if (room != null)
            {
                // Remove the user from the group
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Id.ToString());
                await Clients.Group(room.Id.ToString()).SendAsync("removeUser", userRoom.UserId);
            }

            // Remove the connection mapping
            Connections.Remove(Context.ConnectionId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}