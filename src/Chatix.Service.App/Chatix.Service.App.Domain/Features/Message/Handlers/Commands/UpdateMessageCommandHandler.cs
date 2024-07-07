using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Service.App.Domain.Features.Message.Requests.Commands;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Message.Handlers.Commands;

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Unit>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public UpdateMessageCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var existingMessage = await repositoryManager.Message.GetMessageByIdAsync(request.UpdateMessageDto.Id, false) ?? throw new Exception("Message not found");
        var Message = mapper.Map(request.UpdateMessageDto, existingMessage);

        await repositoryManager.Message.UpdateAsync(Message);

        return Unit.Value;

    }
}