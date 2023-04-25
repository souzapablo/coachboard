using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class User : BaseEntity
{
    public User()
    {
    }

    public User(string nickname, string email, string password)
    {
        Nickname = nickname;
        Email = email;
        Password = password;
        Careers = new List<Career>();
    }

    public string Nickname { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public Role Role { get; private set; } = Role.Standard;
    public List<Career> Careers { get; private set; } = new();

    public void ChangeRole(Role newRole) =>
        Role = newRole;
}