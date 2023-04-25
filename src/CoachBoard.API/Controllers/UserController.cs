using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Application.Features.Users.Queries.FindAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/users")]
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
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);
        
        return Ok(id);
    }
}