using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Fixtures;

public record FixtureView(
    long Id,
    long OpponentId)
{
    public static FixtureView Map(Fixture fixture) =>
        new FixtureView(
            fixture.Id,
            fixture.OpponentId);
};