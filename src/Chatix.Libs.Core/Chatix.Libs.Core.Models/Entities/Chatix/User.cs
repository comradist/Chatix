using Chatix.Libs.Core.Models.Entities.Common;

namespace Chatix.Libs.Core.Models.Entities.Chatix;

public class User : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public ICollection<RoomUser>? UserRooms { get; set; } = [];

    public ICollection<Room>? CreatedRooms { get; set; } = [];

    public ICollection<Message>? Messages { get; set; } = [];
}