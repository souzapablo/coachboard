using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Teams;

public record TeamDetailsView(
    long Id,
    long CareerId,
    string Name,
    string Stadium,
    List<string> Squad)
{
    public static TeamDetailsView Map(Team team) =>
        new TeamDetailsView(
            team.Id,
            team.CareerId,
            team.Name,
            team.Stadium,
            team.Squad.Select(player => player.Name).ToList());
};