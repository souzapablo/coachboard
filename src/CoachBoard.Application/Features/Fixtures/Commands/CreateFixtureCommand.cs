using CoachBoard.Core.Enums;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Commands;

public record CreateFixtureCommand(
    long TeamId,
    long OpponentId,
    FixtureLocation Location,
    Competition Competition,
    List<long> PlayersIds) : IRequest<long>;