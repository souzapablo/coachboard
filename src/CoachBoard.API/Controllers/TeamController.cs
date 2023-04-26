using CoachBoard.Application.Features.Teams.Queries.FindAll;
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

    }
}
