using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Libs.Core.Shared.DTOs.Message;

public class MessageDto : BaseDto
{
    public string Content { get; set; }

    public DateTime? SentAt { get; set; }

    public Guid SenderId { get; set; }

    public Guid ToRoomId { get; set; }
}