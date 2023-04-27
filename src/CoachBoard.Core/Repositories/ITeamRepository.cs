using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Core.Repositories;

public interface ITeamRepository
{
    Task<PaginationResult<Team>> FindAllAsync(string? name, int page);
    Task<Team?> FindByIdAsync(long id);
    Task CreateAsync(Team team);
    Task UpdateAsync(Team team);
}