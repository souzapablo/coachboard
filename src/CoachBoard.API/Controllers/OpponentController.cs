using CoachBoard.Application.Features.Opponents.Commands.Create;
using CoachBoard.Application.Features.Opponents.Commands.Delete;
using CoachBoard.Application.Features.Opponents.Queries.FindAll;
using CoachBoard.Application.Features.Opponents.Queries.FindById;
using CoachBoard.Application.InputModels.Opponents;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/opponents")]
public class OpponentController : ControllerBase
{
    private readonly IMediator _mediator;

    public OpponentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll([FromQuery] FindAllOpponentsQuery query)
    {
        var opponents = await _mediator.Send(query);
        return Ok(opponents);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindById([FromRoute] long id)
    {
        var query = new FindOpponentByIdQuery(id);
        var opponent = await _mediator.Send(query);
        return Ok(opponent);
    }

    [HttpPost("career/{careerId:long}")]
    public async Task<IActionResult> Create([FromRoute] long careerId, [FromBody] CreateOpponentInput input)
    {
        var command = new CreateOpponentCommand(
            careerId,
            input.Name,
            input.Stadium);

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { Id = id }, command);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var command = new DeleteOpponentCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}