using CoachBoard.Application.Features.Fixtures.Commands;
using CoachBoard.Application.InputModels.Fixtures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/fixture")]
public class FixtureController : ControllerBase
{
    private readonly IMediator _mediator;

    public FixtureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindById()
    {
        return Ok();
    }
    
    [HttpPost("team/{teamId:long}")]
    public async Task<IActionResult> Create([FromRoute] long teamId, [FromBody] CreateFixtureInput input)
    {
        var command = new CreateFixtureCommand(
            teamId,
            input.OpponentId,
            input.Location,
            input.Competition,
            input.PlayersIds);

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { Id = id }, command);
    }
}