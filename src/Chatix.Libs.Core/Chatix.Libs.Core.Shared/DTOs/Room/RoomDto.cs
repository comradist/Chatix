using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Libs.Core.Shared.DTOs.Room;

public class RoomDto : BaseDto
{
    public string Name { get; set; }

    public UserDto Admin { get; set; }

    public ICollection<UserDto>? UsersInRoom { get; set; }

    public ICollection<MessageDto>? Messages { get; set; }

}