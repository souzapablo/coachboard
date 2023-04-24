using CoachBoard.Core.Entities;

namespace CoachBoard.Application.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    Task<bool> FindByEmailAsync(string email);
}