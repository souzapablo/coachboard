using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Application.Repositories;

public interface IUserRepository
{
    Task<PaginationResult<User>> FindAllAsync(string? nickname, int page);
    Task CreateAsync(User user);
    Task<bool> FindByEmailAsync(string email);
}