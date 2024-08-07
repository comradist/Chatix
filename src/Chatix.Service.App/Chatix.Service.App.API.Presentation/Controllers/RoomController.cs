using System.Text.Json;
using System.Text.Json.Serialization;
using Chatix.Libs.Core.Contracts.Logger;
using Chatix.Libs.Core.Shared.DTOs.Room;
using Chatix.Service.App.API.Presentation.ActionFilters;
using Chatix.Service.App.API.Presentation.Hubs;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Commands;
using Chatix.Service.App.Domain.Features.Rooms.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatix.Service.App.API.Presentation.Controllers;

[ApiController]

[Route("api/rooms")]
public class RoomController : ControllerBase
{
    private readonly IMediator mediator;

    private readonly IHubContext<ChatHub> hubContext;

    private readonly ILoggerManager logger;

    public RoomController(IMediator mediator, ILoggerManager logger, IHubContext<ChatHub> hubContext)
    {
        this.hubContext = hubContext;
        this.mediator = mediator;
        this.logger = logger;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<List<RoomDto>>> GetRooms()
    {
        var roomsDto = await mediator.Send(new GetRoomsRequest());

        return Ok(roomsDto);
    }

    [HttpGet("{id:Guid}", Name = "GetRoom")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<RoomDto>> GetRoom(Guid id)
    {
        var roomDto = await mediator.Send(new GetRoomByIdRequest { Id = id });

        return Ok(roomDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<RoomDto>> CreateRoom([FromBody] CreateRoomDto createRoomDto)
    {
        var roomDto = await mediator.Send(new CreateRoomCommand { RoomDto = createRoomDto });

        return CreatedAtRoute("GetRoom", new { roomDto.Id }, roomDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateRoom([FromBody] UpdateRoomDto updateRoomDto)
    {
        await mediator.Send(new UpdateRoomCommand { UpdateRoomDto = updateRoomDto });

        //await hubContext.Clients.All.SendAsync("addChatRoom", JsonSerializer.Serialize(roomDto, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles }));

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> DeleteRoom([FromBody] DeleteRoomDto deleteRoomDto)
    {
        await hubContext.Clients.Group(deleteRoomDto.Id.ToString()).SendAsync("CloseRoom", deleteRoomDto.Id, deleteRoomDto.SenderRequestId);

        await mediator.Send(new DeleteRoomCommand { deleteRoomDto = deleteRoomDto });

        return NoContent();
    }
}