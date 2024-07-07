using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities.Chatix;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.Domain.Features.Room.Requests.Commands;
using Chatix.Service.App.Domain.Features.User.Requests.Commands;
using MediatR;

using RoomEntity = Chatix.Libs.Core.Models.Entities.Chatix.Room;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Service.App.Domain.Features.Room.Handlers.Commands;

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
        var room = mapper.Map<RoomEntity>(request.RoomDto);

        var user = await repositoryManager.User.GetUserByIdAsync(request.RoomDto.AdminId, false) ?? throw new Exception("User not found");
        user.CreatedRooms!.Add(room);
        var userForUpdate = mapper.Map<UpdateUserDto>(user);
        await mediator.Send(new UpdateUserCommand { UpdateUserDto = userForUpdate }, cancellationToken);
        
        await repositoryManager.Room.CreateAsync(room);
        var roomForReturn = await repositoryManager.Room.GetRoomByIdAsync(room.Id, false);

        var roomDto = mapper.Map<RoomDto>(roomForReturn);

        return roomDto;
    }
}
