using System.Text.Json;
using System.Text.Json.Serialization;
using Chatix.Libs.Core.Contracts.Logger;
using Chatix.Libs.Core.Shared.DTOs.Message;
using Chatix.Service.App.API.Presentation.ActionFilters;
using Chatix.Service.App.API.Presentation.Hubs;
using Chatix.Service.App.Domain.Features.Messages.Requests.Commands;
using Chatix.Service.App.Domain.Features.Messages.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatix.Service.App.API.Presentation.Controllers;

[ApiController]

[Route("api/messages")]
public class MessageController : ControllerBase
{
    private readonly IMediator mediator;


    private readonly IHubContext<ChatHub> hubContext;

    private readonly ILoggerManager logger;

    public MessageController(IMediator mediator, ILoggerManager logger, IHubContext<ChatHub> hubContext)
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
    public async Task<ActionResult<List<MessageDto>>> GetMessages()
    {
        var MessagesDto = await mediator.Send(new GetMessagesRequest());

        return Ok(MessagesDto);
    }

    [HttpGet("{id:Guid}", Name = "GetMessage")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<MessageDto>> GetMessage(Guid id)
    {
        var MessageDto = await mediator.Send(new GetMessageByIdRequest { Id = id });

        return Ok(MessageDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageDto createMessageDto)
    {
        var messageDto = await mediator.Send(new CreateMessageCommand { MessageDto = createMessageDto });

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true
        };

        await hubContext.Clients.Group(messageDto.ToRoomId.ToString()).SendAsync("sendMessage", JsonSerializer.Serialize(messageDto, options));
        //await hubContext.Clients.All.SendAsync("ReceiveMessage", JsonSerializer.Serialize(messageDto, options));
        return CreatedAtRoute("GetMessage", new { messageDto.Id }, messageDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageDto updateMessageDto)
    {
        await mediator.Send(new UpdateMessageCommand { UpdateMessageDto = updateMessageDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteMessage(Guid id)
    {
        await mediator.Send(new DeleteMessageCommand { Id = id });
        return NoContent();
    }
}