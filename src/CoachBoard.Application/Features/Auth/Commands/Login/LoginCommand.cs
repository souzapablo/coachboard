using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Auth.Commands.Login;
public record LoginCommand(
    string Username,
    string Password) : IRequest<Result<string>>;
