using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Opponent : BaseEntity
{
    public Opponent()
    {
    }

    public Opponent(long careerId, string name, string stadium)
    {
        CareerId = careerId;
        Name = name;
        Stadium = stadium;
        Fixtures = new List<Fixture>();
    }

    public long CareerId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Stadium { get; private set; } = string.Empty;
    public List<Fixture> Fixtures { get; private set; } = new();
}