using System.Text.Json;
using Chatix.Libs.Core.Contracts.Logger;
using Chatix.Libs.Core.Shared.DTOs.User;
using Chatix.Service.App.API.Presentation.ActionFilters;
using Chatix.Service.App.API.Presentation.Hubs;
using Chatix.Service.App.Domain.Features.User.Requests.Commands;
using Chatix.Service.App.Domain.Features.User.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatix.Service.App.API.Presentation.Controllers;

[ApiController]

[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;


    private readonly IHubContext<ChatHub> hubContext;

    private readonly ILoggerManager logger;

    public UserController(IMediator mediator, ILoggerManager logger, IHubContext<ChatHub> hubContext)
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
    public async Task<ActionResult<List<UserDto>>> GetUsers()
    {
        var UsersDto = await mediator.Send(new GetUsersRequest());

        return Ok(UsersDto);
    }

    [HttpGet("{id:Guid}", Name = "GetUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        var UserDto = await mediator.Send(new GetUserByIdRequest { Id = id });

        return Ok(UserDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var userDto = await mediator.Send(new CreateUserCommand { UserDto = createUserDto });
        
        await hubContext.Clients.All.SendAsync("ReceiveUser", JsonSerializer.Serialize(userDto));
        return CreatedAtRoute("GetUser", new { userDto.Id }, userDto);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        await mediator.Send(new UpdateUserCommand { UpdateUserDto = updateUserDto });

        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await mediator.Send(new DeleteUserCommand { Id = id });

        return NoContent();
    }
}