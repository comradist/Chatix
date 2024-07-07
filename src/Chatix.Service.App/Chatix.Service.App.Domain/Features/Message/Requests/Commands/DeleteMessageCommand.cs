using MediatR;

namespace Chatix.Service.App.Domain.Features.Message.Requests.Commands;

public class DeleteMessageCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}