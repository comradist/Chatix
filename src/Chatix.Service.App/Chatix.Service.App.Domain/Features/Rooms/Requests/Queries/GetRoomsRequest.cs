using Chatix.Libs.Core.Shared.DTOs.Room;
using MediatR;

namespace Chatix.Service.App.Domain.Features.Rooms.Requests.Queries;

public class GetRoomsRequest : IRequest<List<RoomDto>>
{
}