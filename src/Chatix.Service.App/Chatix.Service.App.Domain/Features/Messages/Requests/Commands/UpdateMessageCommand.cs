using Chatix.Libs.Core.Shared.DTOs.Message;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Messages.Requests.Commands;

public class UpdateMessageCommand : IRequest<Unit>
{
    public UpdateMessageDto UpdateMessageDto { get; set; }
}