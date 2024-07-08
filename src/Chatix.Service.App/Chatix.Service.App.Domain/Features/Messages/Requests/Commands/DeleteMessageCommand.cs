using MediatR;

namespace Chatix.Service.App.Domain.Features.Messages.Requests.Commands;

public class DeleteMessageCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}