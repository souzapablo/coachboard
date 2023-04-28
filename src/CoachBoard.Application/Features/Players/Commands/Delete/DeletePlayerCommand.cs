using MediatR;

namespace CoachBoard.Application.Features.Players.Commands.Delete;

public record DeletePlayerCommand(
    long Id) : IRequest<Unit>;