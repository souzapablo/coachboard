using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Assist : BaseEntity
{
    public Assist()
    {
    }

    public Assist(long assistFixtureId, long goalId, long playerAssistedId)
    {
        AssistFixtureId = assistFixtureId;
        GoalId = goalId;
        PlayerAssistedId = playerAssistedId;
    }

    public long AssistFixtureId { get; private set; }
    public Fixture Fixture { get; private set; } = null!;
    public long GoalId { get; private set; }
    public Goal Goal { get; private set; } = null!;
    public long PlayerAssistedId { get; private set; }
    public Player PlayerAssisted { get; private set; } = null!;
}