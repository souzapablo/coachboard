using CoachBoard.Domain.Entities.Base;

namespace CoachBoard.Domain.Entities;
public class Career(Guid id, string manager) : Entity(id)
{
    private readonly List<Team> _teams = [];

    public string Manager { get; private set; } = manager;
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public User User { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public IReadOnlyCollection<Team> Teams => _teams;
}
