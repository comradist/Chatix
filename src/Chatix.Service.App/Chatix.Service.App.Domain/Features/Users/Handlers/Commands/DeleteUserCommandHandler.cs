using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Service.App.Domain.Features.Users.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Users.Handlers.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteUserCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var User = await repositoryManager.User.GetUserByIdAsync(request.Id, false) ?? throw new Exception("User not found");

        await repositoryManager.User.DeleteAsync(User);

        return Unit.Value;
    }
}