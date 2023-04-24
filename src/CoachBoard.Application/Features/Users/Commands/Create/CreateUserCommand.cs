using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Create;

public record CreateUserCommand(
    string Nickname,
    string Email,
    string Password
) : IRequest<long>;