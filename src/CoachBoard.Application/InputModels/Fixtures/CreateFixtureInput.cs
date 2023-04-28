using CoachBoard.Core.Enums;

namespace CoachBoard.Application.InputModels.Fixtures;

public record CreateFixtureInput(
    long OpponentId,
    FixtureLocation Location,
    Competition Competition,
    List<long> PlayersIds);