using Chatix.Libs.Core.Shared.DTOs.Message;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Message.Requests.Commands;

public class CreateMessageCommand : IRequest<MessageDto>
{
    public CreateMessageDto MessageDto { get; set; }
}