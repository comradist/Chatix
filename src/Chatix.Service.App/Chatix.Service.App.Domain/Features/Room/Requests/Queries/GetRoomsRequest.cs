using Chatix.Libs.Core.Shared.DTOs.Room;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Room.Requests.Queries;

public class GetRoomsRequest : IRequest<List<RoomDto>>
{
}