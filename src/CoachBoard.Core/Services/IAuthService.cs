namespace CoachBoard.Core.Services;

public interface IAuthService
{
    string GenerateJwtToken(string nickname, string role);
    string ComputeSha256Hash(string password);
}