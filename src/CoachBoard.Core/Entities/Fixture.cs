using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Fixture : BaseEntity
{
    public Fixture()
    {
    }

    public Fixture(long teamId, long opponentId, FixtureLocation location, Competition competition)
    {
        TeamId = teamId;
        OpponentId = opponentId;
        Location = location;
        Competition = competition;
        Goals = new List<Goal>();
        LineUp = new List<Player>();
        Assists = new List<Assist>();
    }

    public long TeamId { get; private set; }
    public Team Team { get; private set; } = null!;
    public FixtureLocation Location { get; private set; }
    public List<Player> LineUp { get; private set; } = new();
    public Competition Competition { get; private set; }
    public List<Goal> Goals { get; private set; } = new();
    public List<Assist> Assists { get; private set; } = new();
    public int OpponentGoals { get; private set; }
    public long OpponentId { get; private set; }
    public Opponent Opponent { get; private set; } = null!;
}