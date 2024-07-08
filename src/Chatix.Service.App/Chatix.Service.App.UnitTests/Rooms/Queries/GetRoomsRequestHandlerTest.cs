using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.Domain.Features.Rooms.Handlers.Queries;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Queries;
using Moq;
using Xunit;

public class GetRoomsRequestHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepositoryManager;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetRoomsRequestHandler _handler;

    public GetRoomsRequestHandlerTests()
    {
        _mockRepositoryManager = new Mock<IRepositoryManager>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetRoomsRequestHandler(_mockRepositoryManager.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsListOfRoomDto()
    {
        // Arrange
        var rooms = new List<Room>
        {
            new Room { Id = Guid.NewGuid(), Name = "Room 1" },
            new Room { Id = Guid.NewGuid(), Name = "Room 2" }
        };
        var roomsDto = new List<RoomDto>
        {
            new RoomDto { Id = rooms[0].Id, Name = "Room 1" },
            new RoomDto { Id = rooms[1].Id, Name = "Room 2" }
        };

        _mockRepositoryManager.Setup(r => r.Room.GetAllAsync(false)).ReturnsAsync(rooms);
        _mockMapper.Setup(m => m.Map<List<RoomDto>>(rooms)).Returns(roomsDto);

        var request = new GetRoomsRequest();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Room 1", result[0].Name);
        Assert.Equal("Room 2", result[1].Name);

        _mockRepositoryManager.Verify(r => r.Room.GetAllAsync(false), Times.Once);
        _mockMapper.Verify(m => m.Map<List<RoomDto>>(rooms), Times.Once);
    }
}
