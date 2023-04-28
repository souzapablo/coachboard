using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Team : BaseEntity
{
    public Team()
    {
    }

    public Team(long careerId, string name, string stadium)
    {
        CareerId = careerId;
        Name = name;
        Stadium = stadium;
        Squad = new List<Player>();
        Fixtures = new List<Fixture>();
        Transfers = new List<Transfer>();
    }

    public long CareerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
    public List<Player> Squad { get; private set; } = new();
    public List<Fixture> Fixtures { get; private set; } = new();
    public List<Transfer> Transfers { get; private set; } = new();
}