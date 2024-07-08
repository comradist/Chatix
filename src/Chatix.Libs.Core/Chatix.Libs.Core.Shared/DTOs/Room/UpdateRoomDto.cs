
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.User;

namespace Chatix.Libs.Core.Shared.DTOs.Room;

public class UpdateRoomDto : BaseDto
{
    public string Name { get; set; }

    public Guid AdminId { get; set; }

    public ICollection<MessageDto>? Messages { get; set; }

    public ICollection<RoomUser>? RoomUsers { get; set; }
}