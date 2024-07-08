using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.Domain.Features.RoomsUsers.Requests.Commands;
using Chatix.Service.App.Domain.Features.Users.Requests.Commands;
using MediatR;


namespace Chatix.Service.App.Domain.Features.RoomsUsers.Handlers.Commands;

public class AddUserToRoomCommandHandler : IRequestHandler<AddUserToRoomCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public AddUserToRoomCommandHandler(IRepositoryManager repositoryManager, IMapper mapper, IMediator mediator)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(AddUserToRoomCommand request, CancellationToken cancellationToken)
    {
        var user = await repositoryManager.User.GetUserByIdAsync(request.RoomUser.UserId, false) ?? throw new Exception("User not found");
        var room = await repositoryManager.Room.GetRoomByIdAsync(request.RoomUser.RoomId, false) ?? throw new Exception("Room not found");
        if (request.RoomUser.UserId == default || request.RoomUser.RoomId == default)
        {
            throw new Exception("User or Room not found");
        }

        var roomUser = new RoomUser { RoomId = request.RoomUser.RoomId, UserId = request.RoomUser.UserId };
        // user.R!.Add(request.RoomUser);
        // room.UserRooms!.Add(request.RoomUser);
        await repositoryManager.RoomUser.CreateAsync(roomUser);
        
        return Unit.Value;
    }
}
