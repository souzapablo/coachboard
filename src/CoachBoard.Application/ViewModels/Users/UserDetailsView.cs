namespace CoachBoard.Application.ViewModels.Users;

public record UserDetailsView(
    long Id,
    string Nickname,
    string Email,
    ICollection<string> Careers
);