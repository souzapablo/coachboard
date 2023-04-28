using CoachBoard.Application.Features.Fixtures.Commands.Create;
using CoachBoard.Application.Features.Fixtures.Commands.CreateGoal;
using CoachBoard.Application.InputModels.Fixtures;
using CoachBoard.Application.InputModels.Goals;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/fixtures")]
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

    [HttpGet("goal/{goalId:long}")]
    public async Task<IActionResult> FindGoal([FromRoute] long goalId)
    {
        return Ok();
    }

    [HttpPost("{id:long}/goal")]
    public async Task<IActionResult> Create([FromRoute] long id, [FromBody] CreateGoalInput input)
    {
        var command = new CreateGoalCommand(
            id,
            input.PlayerScoredId,
            input.PlayerAssistedId,
            input.IsOwnGoal);

        var goalId = await _mediator.Send(command);

        return CreatedAtAction(nameof(FindGoal), new { GoalId = goalId }, command);
    }
}