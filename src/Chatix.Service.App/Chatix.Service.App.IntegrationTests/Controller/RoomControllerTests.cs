using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Chatix.Libs.Core.Shared.DTOs.Room;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Chatix.Service.App.IntegrationTests.Controller
{
    public class RoomControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public RoomControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CreateRoom_ReturnsCreatedResponse()
        {
            // Arrange
            var createRoomDto = new CreateRoomDto { Name = "Test Room", AdminId = Guid.NewGuid() };
            var content = new StringContent(JsonConvert.SerializeObject(createRoomDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/rooms", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<RoomDto>(responseString);

            Assert.Equal("Test Room", room.Name);
        }

        [Fact]
        public async Task GetRoom_ReturnsRoom()
        {
            // Arrange
            var createRoomDto = new CreateRoomDto { Name = "Test Room", AdminId = Guid.NewGuid() };
            var content = new StringContent(JsonConvert.SerializeObject(createRoomDto), Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/rooms", content);
            createResponse.EnsureSuccessStatusCode();
            var createdRoom = JsonConvert.DeserializeObject<RoomDto>(await createResponse.Content.ReadAsStringAsync());

            // Act
            var response = await _client.GetAsync($"/api/rooms/{createdRoom.Id}");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<RoomDto>(responseString);

            Assert.Equal("Test Room", room.Name);
        }

        [Fact]
        public async Task UpdateRoom_ReturnsNoContent()
        {
            // Arrange
            var createRoomDto = new CreateRoomDto { Name = "Test Room", AdminId = Guid.NewGuid() };
            var content = new StringContent(JsonConvert.SerializeObject(createRoomDto), Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/rooms", content);
            createResponse.EnsureSuccessStatusCode();
            var createdRoom = JsonConvert.DeserializeObject<RoomDto>(await createResponse.Content.ReadAsStringAsync());

            var updateRoomDto = new UpdateRoomDto { Id = createdRoom.Id, Name = "Updated Room" };
            var updateContent = new StringContent(JsonConvert.SerializeObject(updateRoomDto), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/rooms", updateContent);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteRoom_ReturnsNoContent()
        {
            // Arrange
            var createRoomDto = new CreateRoomDto { Name = "Test Room", AdminId = Guid.NewGuid() };
            var content = new StringContent(JsonConvert.SerializeObject(createRoomDto), Encoding.UTF8, "application/json");

            var createResponse = await _client.PostAsync("/api/rooms", content);
            createResponse.EnsureSuccessStatusCode();
            var createdRoom = JsonConvert.DeserializeObject<RoomDto>(await createResponse.Content.ReadAsStringAsync());

            var deleteRoomDto = new DeleteRoomDto { Id = createdRoom.Id, SenderRequestId = Guid.NewGuid() };
            var deleteContent = new StringContent(JsonConvert.SerializeObject(deleteRoomDto), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/api/rooms", UriKind.Relative),
                Content = deleteContent
            };

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }
    }
}
