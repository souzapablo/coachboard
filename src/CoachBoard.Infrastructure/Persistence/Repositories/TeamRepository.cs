using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public TeamRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(Team team) =>
        await _dbContext.Teams.AddAsync(team);
}