using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Opponents;

public record OpponentView(
    long Id,
    string Name)
{
    public static OpponentView Map(Opponent opponent) =>
        new(opponent.Id,
            opponent.Name);
};