using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Assist : BaseEntity
{
    public Assist(long goalId, long playerAssistedId)
    {
        GoalId = goalId;
        PlayerAssistedId = playerAssistedId;
    }

    public long GoalId { get; private set; }
    public long PlayerAssistedId { get; private set; }
}