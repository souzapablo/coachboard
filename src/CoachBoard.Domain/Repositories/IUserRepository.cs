using CoachBoard.Domain.Entities;

namespace CoachBoard.Domain.Repositories;
public interface IUserRepository
{
    Task<bool> VerifyIfEmailIsRegisteredAsync(string email, CancellationToken cancellationToken = default);
    void Create(User user);
    void Update(User user);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByUsername(string username, CancellationToken cancellationToken = default);
    IQueryable<User> List();
}
