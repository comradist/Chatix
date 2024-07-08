using AutoMapper;
using MediatR;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.Users.Requests.Queries;

namespace Chatix.Service.App.Domain.Features.Users.Handlers.Queries;

public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, List<UserDto>>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetUsersRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await repositoryManager.User.GetAllAsync(false) ?? throw new Exception("User not found");

        var usersDto = mapper.Map<List<UserDto>>(users);
        return usersDto;
    }
}