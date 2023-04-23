using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Goal : BaseEntity
{
    public Goal(long fixtureId)
    {
        FixtureId = fixtureId;
    }

    public long FixtureId { get; private set; }
    public Assist? Assist { get; private set; }
    public bool IsOwnGoal { get; private set; } = false;
}