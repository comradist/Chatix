
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Libs.Core.Shared.DTOs.Room;

public class CreateRoomDto
{
    public string Name { get; set; }

    public Guid AdminId { get; set; }

}