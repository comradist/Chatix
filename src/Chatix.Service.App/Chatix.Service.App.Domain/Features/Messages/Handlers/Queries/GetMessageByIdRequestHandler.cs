using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Service.App.Domain.Features.Messages.Requests.Queries;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Messages.Handlers.Queries;

public class GetMessageByIdRequestHandler : IRequestHandler<GetMessageByIdRequest, MessageDto>
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IMapper mapper;

    public GetMessageByIdRequestHandler(IRepositoryManager repositoryManager, IMapper mapper)
    {
        this.repositoryManager = repositoryManager;
        this.mapper = mapper;
    }

    public async Task<MessageDto> Handle(GetMessageByIdRequest request, CancellationToken cancellationToken)
    {
        var Message = await repositoryManager.Message.GetMessageByIdAsync(request.Id, false) ?? throw new Exception("Message not found");

        var MessageDto = mapper.Map<MessageDto>(Message);

        return MessageDto;
    }
}