using CoachBoard.Application.Features.Fixtures.Commands.Create;
using CoachBoard.Application.Features.Fixtures.Commands.CreateGoal;
using CoachBoard.Application.Features.Fixtures.Commands.Delete;
using CoachBoard.Application.Features.Fixtures.Queries.FindAll;
using CoachBoard.Application.Features.Fixtures.Queries.FindById;
using CoachBoard.Application.Features.Goals.Queries.FindById;
using CoachBoard.Application.InputModels.Fixtures;
using CoachBoard.Application.InputModels.Goals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/fixtures")]
[Authorize]
public class FixtureController : ControllerBase
{
    private readonly IMediator _mediator;

    public FixtureController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll([FromQuery] FindAllFixturesQuery query)
    {
        var fixtures = await _mediator.Send(query);

        return Ok(fixtures);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindById([FromRoute] long id)
    {
        var query = new FindFixtureByIdQuery(id);
        var fixture = await _mediator.Send(query);
        return Ok(fixture);
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

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var command = new DeleteFixtureCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("goal/{goalId:long}")]
    public async Task<IActionResult> FindGoal([FromRoute] long goalId)
    {
        var query = new FindGoalByIdQuery(goalId);
        var goal = await _mediator.Send(query);
        return Ok(goal);
    }

    [HttpPost("{id:long}/goal")]
    public async Task<IActionResult> Create([FromRoute] long id, [FromBody] CreateGoalInput input)
    {
        var command = new CreateGoalCommand(
            id,
            input.PlayerScoredId,
            input.PlayerAssistedId);

        var goalId = await _mediator.Send(command);

        return CreatedAtAction(nameof(FindGoal), new { GoalId = goalId }, command);
    }
}