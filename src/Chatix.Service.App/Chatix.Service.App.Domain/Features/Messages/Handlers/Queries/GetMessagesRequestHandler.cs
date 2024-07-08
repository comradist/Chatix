using AutoMapper;
using MediatR;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Service.App.Domain.Features.Messages.Requests.Queries;

namespace Chatix.Service.App.Domain.Features.Messages.Handlers.Queries;

public class GetMessagesRequestHandler : IRequestHandler<GetMessagesRequest, List<MessageDto>>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetMessagesRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<List<MessageDto>> Handle(GetMessagesRequest request, CancellationToken cancellationToken)
    {
        var Messages = await repositoryManager.Message.GetAllAsync(false) ?? throw new Exception("Message not found");

        var MessagesDto = mapper.Map<List<MessageDto>>(Messages);
        return MessagesDto;
    }
}