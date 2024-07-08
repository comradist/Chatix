namespace Chatix.Libs.Core.Shared.DTOs.Message;

public class CreateMessageDto
{
    public string Content { get; set; }

    public DateTime? SentAt { get; set; } = DateTime.Now;

    public Guid SenderId { get; set; }

    public Guid ToRoomId { get; set; }
}