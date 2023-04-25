using CoachBoard.Core.Entities.Base;

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
    public List<Career> Careers { get; private set; } = new();
}