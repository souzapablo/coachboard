using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Fixture : BaseEntity
{
    public Fixture(FixtureLocation location, Competition competition, int opponentGoals, Opponent opponent)
    {
        Location = location;
        Competition = competition;
        OpponentGoals = opponentGoals;
        Opponent = opponent;
        Goals = new List<Goal>();
        LineUp = new List<Player>();
    }

    public FixtureLocation Location { get; private set; }
    public List<Player> LineUp { get; private set; }
    public Competition Competition { get; private set; }
    public List<Goal> Goals { get; private set; }
    public int OpponentGoals { get; private set; }
    public Opponent Opponent { get; private set; }
}