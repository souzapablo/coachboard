using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Commands.CreateGoal;

public record CreateGoalCommand(
    long FixtureId,
    long? PlayerScoredId,
    long? PlayerAssistedId) : IRequest<long>;