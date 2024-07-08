using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.Domain.Features.Users.Requests.Commands;
using MediatR;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Commands;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Service.App.Domain.Features.RoomsUsers.Requests.Commands;

namespace Chatix.Service.App.Domain.Features.Rooms.Handlers.Commands;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;
    private readonly IMediator mediator;

    public CreateRoomCommandHandler(IRepositoryManager repositoryManager, IMapper mapper, IMediator mediator)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
        this.mediator = mediator;
    }

    public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = mapper.Map<Room>(request.RoomDto);

        var user = await repositoryManager.User.GetUserByIdAsync(request.RoomDto.AdminId, false) ?? throw new Exception("User not found");
        var userRoom = new RoomUser { RoomId = room.Id, UserId = user.Id };
        room.RoomUsers!.Add(userRoom);
        user.RoomUsers!.Add(userRoom);

        await repositoryManager.Room.CreateAsync(room);
        var roomForReturn = await repositoryManager.Room.GetRoomByIdAsync(room.Id, false);


        user.CreatedRooms!.Add(roomForReturn);
        var userForUpdate = mapper.Map<UpdateUserDto>(user);
        await mediator.Send(new UpdateUserCommand { UpdateUserDto = userForUpdate }, cancellationToken);
        //await mediator.Send(new AddUserToRoomCommand { RoomUser = new RoomUser { RoomId = roomForReturn.Id, UserId = user.Id } }, cancellationToken);


        var roomDto = mapper.Map<RoomDto>(roomForReturn);

        return roomDto;
    }
}
