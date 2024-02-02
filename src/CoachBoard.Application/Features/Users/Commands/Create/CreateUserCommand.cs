using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Create;
public record CreateUserCommand(
    string Username,
    string Email,
    string Password) : IRequest<Result<Guid>>;