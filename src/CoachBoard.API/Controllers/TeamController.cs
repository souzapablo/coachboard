using CoachBoard.Application.Features.Teams.Commands.Delete;
using CoachBoard.Application.Features.Teams.Queries.FindAll;
using CoachBoard.Application.Features.Teams.Queries.FindById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/teams")]
public class TeamController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll([FromQuery] FindAllTeamsQuery query)
    {
        var teams = await _mediator.Send(query);
        return Ok(teams);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindById([FromRoute] long id)
    {
        var query = new FindTeamByIdQuery(id);
        var team = await _mediator.Send(query);
        return Ok(team);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var command = new DeleteTeamCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
    
    
}
