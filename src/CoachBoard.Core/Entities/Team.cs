using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Team : BaseEntity
{
    public Team(string name, string stadium)
    {
        Name = name;
        Stadium = stadium;
        Squad = new List<Player>();
        Fixtures = new List<Fixture>();
        Transfers = new List<Transfer>();
    }

    public string Name { get; private set; }
    public string Stadium { get; private set; }
    public List<Player> Squad { get; private set; }
    public List<Fixture> Fixtures { get; private set; }
    public List<Transfer> Transfers { get; private set; }
}