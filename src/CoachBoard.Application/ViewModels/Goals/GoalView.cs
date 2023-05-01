using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Goals;

public record GoalView(
    long Id,
    long? PlayerScoredId)
{
    public static GoalView Map(Goal goal) =>
        new GoalView(
            goal.Id,
            goal.PlayerScoredId
        );
};