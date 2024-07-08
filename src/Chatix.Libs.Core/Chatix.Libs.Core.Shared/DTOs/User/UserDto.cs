
using System.Text.Json.Serialization;
using Chatix.Libs.Core.Shared.DTOs.Common;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Libs.Core.Shared.DTOs.Room;

namespace Chatix.Libs.Core.Shared.DTOs.User;

public class UserDto : BaseDto
{
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("roomsOfThisUser")]
    public ICollection<RoomDto>? RoomsOfThisUser { get; set; }

    [JsonPropertyName("createdRooms")]
    public ICollection<RoomDto>? CreatedRooms { get; set; }

    [JsonPropertyName("messages")]
    public ICollection<MessageDto>? Messages { get; set; }

}