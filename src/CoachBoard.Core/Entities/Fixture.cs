using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Fixture : BaseEntity
{
    public Fixture(long teamId, long opponentId, FixtureLocation location, Competition competition)
    {
        TeamId = teamId;
        OpponentId = opponentId;
        Location = location;
        Competition = competition;
        Goals = new List<Goal>();
        LineUp = new List<Player>();
    }

    public long TeamId { get; private set; }
    public FixtureLocation Location { get; private set; }
    public List<Player> LineUp { get; private set; }
    public Competition Competition { get; private set; }
    public List<Goal> Goals { get; private set; }
    public int OpponentGoals { get; private set; }
    public long OpponentId { get; private set; }
}