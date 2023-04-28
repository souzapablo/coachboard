using MediatR;

namespace CoachBoard.Application.Features.Opponents.Commands.Create;

public record CreateOpponentCommand(
    long CareerId,
    string Name,
    string Stadium) : IRequest<long>;