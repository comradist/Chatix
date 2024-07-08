using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Rooms.Handlers.Commands;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateRoomCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var existingRoom = await repositoryManager.Room.GetRoomByIdAsync(request.UpdateRoomDto.Id, false) ?? throw new Exception("Room not found");
        var user = await repositoryManager.User.GetUserByIdAsync(request.UpdateRoomDto.AdminId, false) ?? throw new Exception("User not found");

        if (existingRoom.AdminId != user.Id)
        {
            throw new Exception("You are not the admin of this room");
        }

        var room = mapper.Map(request.UpdateRoomDto, existingRoom);

        await repositoryManager.Room.UpdateAsync(room);

        return Unit.Value;

    }
}