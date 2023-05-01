using System.Linq.Expressions;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class FixtureRepository : IFixtureRepository
{
    private readonly CoachBoardDbContext _dbContext;
    private const int PageSize = 10;

    public FixtureRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Fixture>> FindAllAsync(int page)
    {
        var fixtures = _dbContext.Fixtures
            .AsNoTracking()
            .Where(fixture => !fixture.IsDeleted);

        return await fixtures.GetPaged(page, PageSize);
    }

    public async Task<Fixture?> FindByIdAsync(long id, params Expression<Func<Fixture, object?>>[]? includes)
    {
        return await _dbContext.Fixtures
            .IncludeMultiple(includes)
            .AsNoTracking()
            .AsSplitQuery()
            .SingleOrDefaultAsync(fixture => !fixture.IsDeleted &&
                                             fixture.Id == id);
    }


    public async Task CreateAsync(Fixture fixture) =>
        await _dbContext.Fixtures.AddAsync(fixture);

    public void Update(Fixture fixture) =>
        _dbContext.Fixtures.Update(fixture);

    public async Task SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync();
}