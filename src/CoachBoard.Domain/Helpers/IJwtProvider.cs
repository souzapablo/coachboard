using CoachBoard.Domain.Entities;

namespace CoachBoard.Domain.Helpers;
public interface IJwtProvider
{
    string GenerateToken(User user);
}
