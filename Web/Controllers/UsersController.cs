using Application.Commands.Users;
using Application.Dto;
using Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<List<UserDto>> GetAll()
    {
        var query = new GetAllUsersQuery();
        var users = await mediator.Send(query);

        return users;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserDto>> Get(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await mediator.Send(query);

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Create(CreateUserCommand command)
    {
        var user = await mediator.Send(command);

        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(Guid id)
    {
        await mediator.Send(new ActivateUserCommand(id));

        return Ok();
    }
}