using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Libs.Core.Shared.DTOs.Message;

public class MessageDto : BaseDto
{
    public string Content { get; set; }

    public DateTime SentAt { get; set; }

    public UserDto Sender { get; set; }

    public RoomDto ToRoom { get; set; }
}