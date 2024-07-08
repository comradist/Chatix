using Chatix.Libs.Core.Models.Entities.Common;

namespace Chatix.Libs.Core.Models.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public ICollection<RoomUser>? RoomUsers { get; set; } = [];

    public ICollection<Room>? CreatedRooms { get; set; } = [];

    public ICollection<Message>? Messages { get; set; } = [];
}