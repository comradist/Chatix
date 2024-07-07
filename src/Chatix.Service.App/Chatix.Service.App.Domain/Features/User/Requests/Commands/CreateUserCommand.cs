using Chatix.Libs.Core.Shared.DTOs.User;
using MediatR;

namespace Chatix.Service.App.Domain.Features.User.Requests.Commands;

public class CreateUserCommand : IRequest<UserDto>
{
    public CreateUserDto UserDto { get; set; }
}