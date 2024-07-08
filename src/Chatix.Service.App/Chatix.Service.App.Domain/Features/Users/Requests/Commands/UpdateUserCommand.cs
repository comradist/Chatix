using Chatix.Libs.Core.Shared.DTOs.User;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Users.Requests.Commands;

public class UpdateUserCommand : IRequest<Unit>
{
    public UpdateUserDto UpdateUserDto { get; set; }
}