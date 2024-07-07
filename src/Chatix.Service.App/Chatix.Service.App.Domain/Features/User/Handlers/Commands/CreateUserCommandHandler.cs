using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities.Chatix;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.User.Requests.Commands;
using MediatR;

using UserEntity = Chatix.Libs.Core.Models.Entities.Chatix.User;

namespace Chatix.Service.App.Domain.Features.User.Handlers.Commands;

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
        var user = mapper.Map<UserEntity>(request.UserDto);
        // if (request.UserDto.ProjectIds != null )
        // {
        //     User.Id = Guid.NewGuid();

        //     foreach (var projectId in request.UserDto.ProjectIds)
        //     {
        //         if (projectId == null)
        //         {
        //             continue;
        //         }
        //         var projectUser = new ProjectUser()
        //         {
        //             UserId = User.Id,
        //             ProjectId = projectId
        //         };

        //         User.ProjectUsers.Add(projectUser);
        //     }
        // }

        await repositoryManager.User.CreateAsync(user);
        var userForReturn = await repositoryManager.User.GetUserByIdAsync(user.Id, false);

        var UserDto = mapper.Map<UserDto>(userForReturn);

        return UserDto;
    }
}
