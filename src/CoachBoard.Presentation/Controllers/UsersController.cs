﻿using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Application.Features.Users.Commands.Delete;
using CoachBoard.Application.Features.Users.Queries.GetById;
using CoachBoard.Application.Features.Users.Queries.List;
using CoachBoard.Presentation.InputModels.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.Presentation.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List(int page = 1, int pageSize = 10)
    {
        var result = await sender.Send(new ListUsersQuery(page, pageSize));

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetUserByIdQuery(id);

        var result = await sender.Send(query);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserInputModel input)
    {
        var command = new CreateUserCommand(input.Username, input.Email, input.Password);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return CreatedAtAction(nameof(GetById), new { Id = result.Data }, command);  
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand(id);

        var result = await sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result);

        return NoContent();
    }
}
