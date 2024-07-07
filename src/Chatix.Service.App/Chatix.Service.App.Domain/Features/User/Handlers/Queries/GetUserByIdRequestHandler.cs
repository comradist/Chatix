using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.User.Requests.Queries;
using MediatR;

namespace Chatix.Service.App.Domain.Features.User.Handlers.Queries;

public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, UserDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetUserByIdRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await repositoryManager.User.GetUserByIdAsync(request.Id, false) ?? throw new Exception("User not found");

        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }
}