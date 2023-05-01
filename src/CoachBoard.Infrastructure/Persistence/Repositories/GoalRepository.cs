using System.Linq.Expressions;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public GoalRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Goal?> FindByIdAsync(long id, params Expression<Func<Goal, object?>>[]? includes) =>
        await _dbContext.Goals
            .IncludeMultiple(includes)
            .AsNoTracking()
            .SingleOrDefaultAsync(goal => !goal.IsDeleted &&
                                          goal.Id == id);

    public async Task CreateAsync(Goal goal) =>
        await _dbContext.AddAsync(goal);

    public void Update(Goal goal) =>
        _dbContext.Goals.Update(goal);
}