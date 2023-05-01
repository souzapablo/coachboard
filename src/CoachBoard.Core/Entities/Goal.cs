using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Goal : BaseEntity
{
    public Goal()
    {
    }

    public Goal(long fixtureId, long? playerScoredId = null, long? assistId = null)
    {
        PlayerScoredId = playerScoredId;
        FixtureId = fixtureId;
        AssistId = assistId;
    }

    public Goal(long fixtureId)
    {
        FixtureId = fixtureId;
        IsOwnGoal = true;
    }

    public long? PlayerScoredId { get; private set; }
    public Player? PlayerScored { get; private set; }
    public long FixtureId { get; private set; }
    public Fixture Fixture { get; private set; } = null!;
    public long? AssistId { get; private set; }
    public Assist? Assist { get; private set; }
    public bool IsOwnGoal { get; private set; }

    public void AddAssist(Assist assist)
    {
        Assist = assist;
        AssistId = assist.Id;
    }
}