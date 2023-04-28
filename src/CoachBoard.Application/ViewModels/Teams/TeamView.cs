using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Teams;

public record TeamView(
    long Id,
    long CareerId,
    string Name)
{
    public static TeamView Map(Team team) =>
        new(
            team.Id,
            team.CareerId,
            team.Name);
};
