
using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.Room;

namespace Chatix.Libs.Core.Shared.DTOs.User;

public class UserDto : BaseDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public ICollection<RoomDto>? UserRooms { get; set; }

    public ICollection<RoomDto>? CreatedRooms { get; set; }

    public ICollection<MessageDto>? Messages { get; set; }

}