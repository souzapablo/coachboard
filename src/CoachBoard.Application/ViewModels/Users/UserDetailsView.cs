using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.Application.ViewModels.Users;

public record UserDetailsView(
    long Id,
    string Nickname,
    string Email,
    IEnumerable<string> Careers,
    Role Role
)
{
    public static UserDetailsView Map(User user) =>
        new(
            user.Id,
            user.Nickname,
            user.Email,
            user.Careers.Select(career => career.ManagerName),
            user.Role);
};