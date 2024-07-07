using Chatix.Libs.Core.Models.Entities.Common;

namespace Chatix.Libs.Core.Models.Entities.Chatix;

public class Room : BaseEntity
{
    public string Name { get; set; }

    public Guid AdminId { get; set; }
    
    public User Admin { get; set; }

    public ICollection<RoomUser>? UserRooms { get; set; }

    public ICollection<Message>? Messages { get; set; }
}