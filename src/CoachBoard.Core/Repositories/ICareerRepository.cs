using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Core.Repositories;

public interface ICareerRepository
{
    Task<PaginationResult<Career>> FindAllAsync(string? managerName, int page);
    Task<PaginationResult<Career>> FindByUserIdAsync(long userId, int page);
    Task<Career?> FindByIdAsync(long id);
    Task CreateAsync(Career career);
    Task UpdateAsync(Career career);
}