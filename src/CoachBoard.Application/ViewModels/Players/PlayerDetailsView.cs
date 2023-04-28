using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.Application.ViewModels.Players;

public record PlayerDetailsView(
    long Id,
    long TeamId,
    string Name,
    int Age,
    int Overall,
    int? KitNumber,
    PlayerPosition Position,
    DateTime? JoinedDate,
    PlayerStatus Status,
    int PlayerFixtures,
    int PlayerGoals,
    int PlayerAssists)
{
    public static PlayerDetailsView Map(Player player) =>
        new PlayerDetailsView(
        player.Id,
        player.TeamId,
        player.Name,
        player.Age,
        player.Overall,
        player.KitNumber,
        player.Position,
        player.JoinedDate,
        player.Status,
        player.Fixtures.Count,
        player.Goals.Count,
        player.Assists.Count);
};