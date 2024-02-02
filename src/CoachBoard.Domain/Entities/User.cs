using CoachBoard.Domain.Entities.Base;

namespace CoachBoard.Domain.Entities;
public class User(Guid id, string username, string email, string passwordHash) : Entity(id)
{
    private readonly List<Career> _careers = [];

    public string Username { get; private set; } = username;
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
    public IReadOnlyCollection<Career> Careers => _careers;
}
