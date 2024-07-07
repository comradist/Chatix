using AutoMapper;
using MediatR;
using Chatix.Service.App.Domain.Features.Room.Requests.Queries;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.Room;

namespace Chatix.Service.App.Domain.Features.Room.Handlers.Queries;

public class GetRoomsRequestHandler : IRequestHandler<GetRoomsRequest, List<RoomDto>>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetRoomsRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<List<RoomDto>> Handle(GetRoomsRequest request, CancellationToken cancellationToken)
    {
        var rooms = await repositoryManager.Room.GetAllAsync(false) ?? throw new Exception("Room not found");

        var roomsDto = mapper.Map<List<RoomDto>>(rooms);
        return roomsDto;
    }
}