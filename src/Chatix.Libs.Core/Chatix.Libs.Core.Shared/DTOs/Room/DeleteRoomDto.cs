using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Libs.Core.Shared.DTOs.Room;

public class DeleteRoomDto : BaseDto
{
    public Guid SenderRequestId { get; set; }
}