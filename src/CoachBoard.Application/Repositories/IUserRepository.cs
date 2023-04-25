using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Application.Repositories;

public interface IUserRepository
{
    Task<PaginationResult<User>> FindAllAsync(string? nickname, int page);
    Task<User?> FindByIdAsync(long id);
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> FindByEmailAsync(string email);
}