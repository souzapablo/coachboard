using System.Linq.Expressions;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Core.Repositories;

public interface IFixtureRepository
{
    Task<PaginationResult<Fixture>> FindAllAsync(int page);
    Task<Fixture?> FindByIdAsync(long id, params Expression<Func<Fixture, object?>>[]? includes);
    Task CreateAsync(Fixture fixture);
    void Update(Fixture fixture);
    Task SaveChangesAsync();
}