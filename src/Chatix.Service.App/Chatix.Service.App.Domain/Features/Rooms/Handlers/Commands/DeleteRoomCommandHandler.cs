using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Rooms.Handlers.Commands;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, RoomDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteRoomCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<RoomDto> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await repositoryManager.Room.GetRoomByIdAsync(request.deleteRoomDto.Id, false) ?? throw new Exception("Room not found");
        var user = await repositoryManager.User.GetUserByIdAsync(request.deleteRoomDto.SenderRequestId, false) ?? throw new Exception("User not found");

        if (room.AdminId != user.Id)
        {
            throw new Exception("You are not the admin of this room");
        }


        await repositoryManager.Room.DeleteAsync(room);


        var roomForReturn = mapper.Map<RoomDto>(room);
        return roomForReturn;
    }
}