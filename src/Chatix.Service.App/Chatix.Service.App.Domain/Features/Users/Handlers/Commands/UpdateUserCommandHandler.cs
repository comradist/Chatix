using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Service.App.Domain.Features.Users.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Users.Handlers.Commands;

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
        var existingUser = await repositoryManager.User.GetUserByIdAsync(request.UpdateUserDto.Id, true) ?? throw new Exception("User not found");
        await repositoryManager.SaveChangesAsync();
        var user = mapper.Map(request.UpdateUserDto, existingUser);
        //await repositoryManager.User.UpdateAsync(user);

        return Unit.Value;

    }
}