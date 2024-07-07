using MediatR;

namespace Chatix.Service.App.Domain.Features.User.Requests.Commands;

public class DeleteUserCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}