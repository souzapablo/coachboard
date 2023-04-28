using MediatR;

namespace CoachBoard.Application.Features.Opponents.Commands.Delete;

public record DeleteOpponentCommand(
    long Id) : IRequest<Unit>;