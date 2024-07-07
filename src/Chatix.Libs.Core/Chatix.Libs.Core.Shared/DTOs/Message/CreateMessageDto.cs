namespace Chatix.Libs.Core.Shared.DTOs.Message;

public class CreateMessageDto
{
    public string Content { get; set; }

    public DateTime? SentAt { get; set; }

    public Guid Sender { get; set; }

    public Guid ToRoom { get; set; }
}