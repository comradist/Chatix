using Chatix.Libs.Core.Models.Entities.Common;

namespace Chatix.Libs.Core.Models.Entities.Chatix;

public class Message : BaseEntity
{
    public string Content { get; set; }

    public DateTime SentAt { get; set; }

    public Guid SenderId { get; set; }

    public User Sender { get; set; }

    public Guid ToRoomId { get; set; }

    public Room ToRoom { get; set; }
}