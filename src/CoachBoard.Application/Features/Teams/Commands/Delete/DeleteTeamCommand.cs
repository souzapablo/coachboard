using MediatR;

namespace CoachBoard.Application.Features.Teams.Commands.Delete;

public record DeleteTeamCommand(
    long Id) : IRequest<Unit>;