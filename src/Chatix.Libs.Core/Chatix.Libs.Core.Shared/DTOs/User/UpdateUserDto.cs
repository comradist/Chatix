
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.Room;

namespace Chatix.Libs.Core.Shared.DTOs.User;

public class UpdateUserDto : BaseDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    
    public ICollection<RoomDto>? RoomsOfThisUser { get; set; }

    public ICollection<RoomDto>? CreatedRooms { get; set; }

    public ICollection<MessageDto>? Messages { get; set; }


    public ICollection<RoomUser>? RoomUsers { get; set; }

}