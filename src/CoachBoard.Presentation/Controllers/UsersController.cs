using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Presentation.InputModels.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.Presentation.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserInputModel input)
    {
        var command = new CreateUserCommand(input.Username, input.Email, input.Password);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return Ok(result);  
    }
}
