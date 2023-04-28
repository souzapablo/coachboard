using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Goal : BaseEntity
{
    public Goal(long fixtureId, long playerScoredId)
    {
        PlayerScoredId = playerScoredId;
        FixtureId = fixtureId;
    }

    public long PlayerScoredId { get; private set; }
    public Player PlayerScored { get; private set; } = null!;
    public long FixtureId { get; private set; }
    public Assist? Assist { get; private set; }
    public bool IsOwnGoal { get; private set; }
}