using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public GoalRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Goal goal) =>
        await _dbContext.AddAsync(goal);
}