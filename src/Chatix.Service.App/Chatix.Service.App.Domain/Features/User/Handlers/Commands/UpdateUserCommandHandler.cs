using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Service.App.Domain.Features.User.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.User.Handlers.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateUserCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await repositoryManager.User.GetUserByIdAsync(request.UpdateUserDto.Id, false) ?? throw new Exception("User not found");
        var user = mapper.Map(request.UpdateUserDto, existingUser);

        await repositoryManager.User.UpdateAsync(user);

        return Unit.Value;

    }
}