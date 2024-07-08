using AutoMapper;
using Chatix.Libs.Core.Contracts.Persistence;
using Chatix.Libs.Core.Models.Entities;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.Domain.Features.Rooms.Handlers.Commands;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Commands;
using Chatix.Service.App.Domain.Features.Users.Requests.Commands;
using MediatR;
using Moq;
using Xunit;

public class CreateRoomCommandHandlerTests
{
    private readonly Mock<IRepositoryManager> _mockRepositoryManager;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMediator> _mockMediator;
    private readonly CreateRoomCommandHandler _handler;

    public CreateRoomCommandHandlerTests()
    {
        _mockRepositoryManager = new Mock<IRepositoryManager>();
        _mockMapper = new Mock<IMapper>();
        _mockMediator = new Mock<IMediator>();
        _handler = new CreateRoomCommandHandler(_mockRepositoryManager.Object, _mockMapper.Object, _mockMediator.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsRoomDto()
    {
        // Arrange
        var createRoomDto = new CreateRoomDto { AdminId = Guid.NewGuid(), Name = "Test Room" };
        var createRoomCommand = new CreateRoomCommand { RoomDto = createRoomDto };
        var room = new Room { Id = Guid.NewGuid(), Name = createRoomDto.Name, RoomUsers = new List<RoomUser>() };
        var user = new User { Id = createRoomDto.AdminId, RoomUsers = new List<RoomUser>(), CreatedRooms = new List<Room>() };
        var roomDto = new RoomDto { Id = room.Id, Name = room.Name };

        _mockMapper.Setup(m => m.Map<Room>(createRoomDto)).Returns(room);
        _mockRepositoryManager.Setup(r => r.User.GetUserByIdAsync(createRoomDto.AdminId, false)).ReturnsAsync(user);
        _mockRepositoryManager.Setup(r => r.Room.CreateAsync(It.IsAny<Room>())).Returns(Task.CompletedTask);
        _mockRepositoryManager.Setup(r => r.Room.GetRoomByIdAsync(room.Id, false)).ReturnsAsync(room);
        _mockMapper.Setup(m => m.Map<RoomDto>(room)).Returns(roomDto);
        _mockMapper.Setup(m => m.Map<UpdateUserDto>(user)).Returns(new UpdateUserDto { Id = user.Id, FirstName = "Updated User" });
        _mockMediator.Setup(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Unit.Value);

        // Act
        var result = await _handler.Handle(createRoomCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(roomDto.Id, result.Id);
        Assert.Equal(roomDto.Name, result.Name);

        _mockRepositoryManager.Verify(r => r.User.GetUserByIdAsync(createRoomDto.AdminId, false), Times.Once);
        _mockRepositoryManager.Verify(r => r.Room.CreateAsync(It.IsAny<Room>()), Times.Once);
        _mockRepositoryManager.Verify(r => r.Room.GetRoomByIdAsync(room.Id, false), Times.Once);
        _mockMediator.Verify(m => m.Send(It.IsAny<UpdateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
