using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Extensions;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly CoachBoardDbContext _dbContext;
    private const int PageSize = 10;
    public TeamRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Team>> FindAllAsync(string? name, int page)
    {
        var teams = _dbContext.Teams
            .Where(team => !team.IsDeleted);

        if (!string.IsNullOrEmpty(name))
            teams = teams.Where(team =>
                team.Name.ToLower()
                .Contains(name.ToLower()));

        return await teams.GetPaged(page, PageSize);
    }

    public async Task CreateAsync(Team team) =>
        await _dbContext.Teams.AddAsync(team);


}