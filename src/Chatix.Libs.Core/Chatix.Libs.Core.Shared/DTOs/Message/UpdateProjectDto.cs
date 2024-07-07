using Chatix.Libs.Core.Shared.DTOs.Common;

namespace Chatix.Libs.Core.Shared.DTOs.Message;

public class UpdateMessageDto : BaseDto
{
    public string Content { get; set; }

    public DateTime SentAt { get; set; }

    public Guid Sender { get; set; }

    public Guid ToRoom { get; set; }
}