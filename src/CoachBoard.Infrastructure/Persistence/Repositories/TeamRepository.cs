using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Team?> FindByIdAsync(long id) =>
        await _dbContext.Teams
            .AsNoTracking()
            .Include(team => team.Squad)
            .Where(team => !team.IsDeleted)
            .FirstOrDefaultAsync(team => team.Id == id);


    public async Task CreateAsync(Team team) =>
        await _dbContext.Teams.AddAsync(team);

    public async Task UpdateAsync(Team team)
    {
        _dbContext.Teams.Update(team);
        await _dbContext.SaveChangesAsync();
    }
}