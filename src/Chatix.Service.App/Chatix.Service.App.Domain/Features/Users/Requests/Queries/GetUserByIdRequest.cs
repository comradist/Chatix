using Chatix.Libs.Core.Shared.DTOs.User;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Users.Requests.Queries;

public class GetUserByIdRequest : IRequest<UserDto>
{
    public Guid Id { get; set; }
}