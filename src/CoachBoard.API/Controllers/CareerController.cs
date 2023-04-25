using CoachBoard.Application.Features.Careers.Queries.FindAll;
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
}