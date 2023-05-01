using CoachBoard.Application.ViewModels.Assists;
using CoachBoard.Application.ViewModels.Goals;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.Application.ViewModels.Fixtures;

public record FixtureDetailsView(
    long Id,
    long TeamId,
    long OpponentId,
    FixtureLocation Location,
    Competition Competition,
    IEnumerable<GoalView> Goals,
    IEnumerable<AssistView> Assists,
    IEnumerable<string> LineUp)
{
    public static FixtureDetailsView Map(Fixture fixture) =>
        new FixtureDetailsView(
            fixture.Id,
            fixture.TeamId,
            fixture.OpponentId,
            fixture.Location,
            fixture.Competition,
            fixture.Goals.Select(GoalView.Map),
            fixture.Assists.Select(AssistView.Map),
            fixture.LineUp.Select(player => player.Name));
};