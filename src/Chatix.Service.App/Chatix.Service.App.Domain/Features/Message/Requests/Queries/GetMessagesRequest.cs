using Chatix.Libs.Core.Shared.DTOs.Message;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Message.Requests.Queries;

public class GetMessagesRequest : IRequest<List<MessageDto>>
{
}