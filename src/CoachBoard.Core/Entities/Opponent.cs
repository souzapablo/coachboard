using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Opponent : BaseEntity
{
    public Opponent()
    {
    }

    public Opponent(string name, string stadium)
    {
        Name = name;
        Stadium = stadium;
        Fixtures = new List<Fixture>();
    }

    public string Name { get; private set; }
    public string Stadium { get; private set; }
    public List<Fixture> Fixtures { get; private set; }
}