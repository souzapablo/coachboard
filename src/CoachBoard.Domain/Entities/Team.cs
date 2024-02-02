using CoachBoard.Domain.Entities.Base;

namespace CoachBoard.Domain.Entities;
public class Team(Guid id, string name, string stadium) : Entity(id)
{
    private readonly List<Team> _teams = [];

    public string Name { get; private set; } = name;
    public string Stadium { get; private set; } = stadium;
    public Guid CareerId { get; private set; }
    public Career Career { get; private set; } = null!;
    public IReadOnlyCollection<Team> Teams => _teams;
}
