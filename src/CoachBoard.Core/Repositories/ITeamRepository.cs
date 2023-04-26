using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Core.Repositories;

public interface ITeamRepository
{
    Task<PaginationResult<Team>> FindAllAsync(string? name, int page);
    Task CreateAsync(Team team);
}