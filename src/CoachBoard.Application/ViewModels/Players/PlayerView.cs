using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.Application.ViewModels.Players;

public record PlayerView(
    long Id,
    long TeamId,
    string Name,
    int Age,
    int Overall,
    PlayerPosition Position)
{
    public static PlayerView? Map(Player player) =>
        new PlayerView(
            player.Id,
            player.TeamId,
            player.Name,
            player.Age,
            player.Overall,
            player.Position);
};