using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Users;

public record UserDetailsView(
    long Id,
    string Nickname,
    string Email,
    IEnumerable<string> Careers
)
{
    public static UserDetailsView Map(User user) =>
        new UserDetailsView(
            user.Id,
            user.Nickname,
            user.Email,
            user.Careers.Select(career => career.ManagerName));
};