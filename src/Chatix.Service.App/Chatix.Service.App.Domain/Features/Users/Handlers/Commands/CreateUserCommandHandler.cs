using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.Users.Requests.Commands;
using MediatR;


namespace Chatix.Service.App.Domain.Features.Users.Handlers.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.UserDto);

        await repositoryManager.User.CreateAsync(user);
        var userForReturn = await repositoryManager.User.GetUserByIdAsync(user.Id, false);

        var UserDto = mapper.Map<UserDto>(userForReturn);

        return UserDto;
    }
}
