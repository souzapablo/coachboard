using CoachBoard.Application.Features.Users.Commands.ChangeRole;
using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Application.Features.Users.Commands.Delete;
using CoachBoard.Application.Features.Users.Queries.FindAll;
using CoachBoard.Application.Features.Users.Queries.FindById;
using CoachBoard.Application.InputModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/users")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll([FromQuery] FindAllUsersQuery query)
    {
        var users = await _mediator.Send(query);
        return Ok(users);
    }
    
    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindById([FromRoute] long id)
    {
        var query = new FindUserByIdQuery(id);
        var user = await _mediator.Send(query);
        return Ok(user);
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { Id = id }, command);
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> UpdateRole([FromRoute] long id, [FromBody] ChangeRoleInput input)
    {
        var command = new ChangeRoleCommand(id, input.Role);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var command = new DeleteUserCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}