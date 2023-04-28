using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Opponents;

public record OpponentDetailsView(
    long Id,
    long CareerId,
    string Name,
    string Stadium,
    int PlayedAgainst)
{
    public static OpponentDetailsView Map(Opponent opponent) =>
        new(opponent.Id,
            opponent.CareerId,
            opponent.Name,
            opponent.Stadium,
            opponent.Fixtures.Count);
};