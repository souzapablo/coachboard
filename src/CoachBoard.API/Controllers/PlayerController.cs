using CoachBoard.Application.Features.Players.Commands.Create;
using CoachBoard.Application.Features.Players.Commands.Delete;
using CoachBoard.Application.Features.Players.Queries.FindAll;
using CoachBoard.Application.Features.Players.Queries.FindById;
using CoachBoard.Application.InputModels.Players;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/players")]
[Authorize]
public class PlayerController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll([FromQuery] FindAllPlayersQuery query)
    {
        var players = await _mediator.Send(query);
        return Ok(players);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindById([FromRoute] long id)
    {
        var query = new FindPlayerByIdQuery(id);
        var player = await _mediator.Send(query);
        return Ok(player);
    }

    [HttpPost("team/{teamId:long}")]
    public async Task<IActionResult> Create([FromRoute] long teamId, [FromBody] CreatePlayerInput input)
    {
        var command = new CreatePlayerCommand(
            teamId,
            input.Name,
            input.JoinedDate,
            input.BirthDate,
            input.Overall,
            input.Position,
            input.KitNumber,
            input.Status);

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(FindById), new { Id = id }, command);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var command = new DeletePlayerCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}