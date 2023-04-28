namespace CoachBoard.Application.InputModels.Goals;

public record CreateGoalInput(
    long PlayerScoredId,
    long? PlayerAssistedId,
    bool IsOwnGoal);