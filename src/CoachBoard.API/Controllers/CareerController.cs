using CoachBoard.Application.Features.Careers.Commands.Create;
using CoachBoard.Application.Features.Careers.Queries.FindAll;
using CoachBoard.Application.Features.Careers.Queries.FindById;
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


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCareerCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(FindById), new { Id = id}, command);
    }
}