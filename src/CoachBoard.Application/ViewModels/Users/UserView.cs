using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.Application.ViewModels.Users;

public record UserView(
    long Id,
    string Nickname,
    string Email,
    Role Role)
{
    public static UserView Map(User user) =>
        new UserView(
            user.Id,
            user.Nickname,
            user.Email,
            user.Role);
};