namespace CoachBoard.Domain.Repositories;
public interface IUserRepository
{
    Task<bool> VerifyIfEmailIsRegisteredAsync(string email, CancellationToken cancellationToken = default);
}
