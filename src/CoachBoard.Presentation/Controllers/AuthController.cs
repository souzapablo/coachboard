using CoachBoard.Application.Features.Auth.Commands.Login;
using CoachBoard.Presentation.InputModels.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.Presentation.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(ISender _sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginInputModel input)
    {
        var command = new LoginCommand(input.Username, input.Password);

        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }
}
