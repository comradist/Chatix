using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Service.App.Domain.Features.Messages.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Messages.Handlers.Commands;

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public DeleteMessageCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var Message = await repositoryManager.Message.GetMessageByIdAsync(request.Id, false) ?? throw new Exception("Message not found");

        await repositoryManager.Message.DeleteAsync(Message);

        return Unit.Value;
    }
}