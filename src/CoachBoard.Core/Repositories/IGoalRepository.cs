using System.Linq.Expressions;
using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IGoalRepository
{
    Task<Goal?> FindByIdAsync(long id, params Expression<Func<Goal, object?>>[]? includes);
    Task CreateAsync(Goal goal);
    void Update(Goal goal);
}