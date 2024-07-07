using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.Domain.Features.Room.Requests.Queries;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Room.Handlers.Queries;

public class GetRoomByIdRequestHandler : IRequestHandler<GetRoomByIdRequest, RoomDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetRoomByIdRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<RoomDto> Handle(GetRoomByIdRequest request, CancellationToken cancellationToken)
    {
        var room = await repositoryManager.Room.GetRoomByIdAsync(request.Id, false) ?? throw new Exception("Room not found");

        var roomDto = mapper.Map<RoomDto>(room);

        return roomDto;
    }
}