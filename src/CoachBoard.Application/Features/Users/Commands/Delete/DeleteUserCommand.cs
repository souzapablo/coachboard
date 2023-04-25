using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Delete;

public record DeleteUserCommand(
    long Id) : IRequest<Unit>;