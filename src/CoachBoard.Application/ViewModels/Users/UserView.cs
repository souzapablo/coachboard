using CoachBoard.Core.Entities;

namespace CoachBoard.Application.ViewModels.Users;

public record UserView(
    long Id,
    string Nickname,
    string Email)
{
    public static UserView Map(User user) =>
        new UserView(
            user.Id,
            user.Nickname,
            user.Email);
};