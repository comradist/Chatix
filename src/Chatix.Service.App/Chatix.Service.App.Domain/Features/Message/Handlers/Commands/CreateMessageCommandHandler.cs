using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities.Chatix;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Service.App.Domain.Features.Message.Requests.Commands;
using MediatR;

using MessageEntity = Chatix.Libs.Core.Models.Entities.Chatix.Message;

namespace Chatix.Service.App.Domain.Features.Message.Handlers.Commands;

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, MessageDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public CreateMessageCommandHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var Message = mapper.Map<MessageEntity>(request.MessageDto);
        // if (request.MessageDto.ProjectIds != null )
        // {
        //     Message.Id = Guid.NewGuid();

        //     foreach (var projectId in request.MessageDto.ProjectIds)
        //     {
        //         if (projectId == null)
        //         {
        //             continue;
        //         }
        //         var projectMessage = new ProjectMessage()
        //         {
        //             MessageId = Message.Id,
        //             ProjectId = projectId
        //         };

        //         Message.ProjectMessages.Add(projectMessage);
        //     }
        // }

        await repositoryManager.Message.CreateAsync(Message);
        var MessageForReturn = await repositoryManager.Message.GetMessageByIdAsync(Message.Id, false);

        var MessageDto = mapper.Map<MessageDto>(MessageForReturn);

        return MessageDto;
    }
}
