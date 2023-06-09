using CoachBoard.Application.Features.Careers.Commands.Create;
using CoachBoard.Application.Features.Careers.Commands.Delete;
using CoachBoard.Application.Features.Careers.Queries.FindAll;
using CoachBoard.Application.Features.Careers.Queries.FindById;
using CoachBoard.Application.Features.Careers.Queries.FindByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Controllers;

[ApiController]
[Route("api/v1/careers")]
[Authorize]
public class CareerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CareerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll([FromQuery] FindAllCareersQuery query)
    {
        var careers = await _mediator.Send(query);

        return Ok(careers);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> FindById([FromRoute] long id)
    {
        var query = new FindCareerByIdQuery(id);
        var career = await _mediator.Send(query);
        return Ok(career);
    }
    
    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> FindByUserId([FromRoute] long userId, [FromQuery] int page = 1)
    {
        var query = new FindCareersByUserIdQuery(userId, page);
        var career = await _mediator.Send(query);
        return Ok(career);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCareerCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(FindById), new { Id = id }, command);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        var command = new DeleteCareerCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}