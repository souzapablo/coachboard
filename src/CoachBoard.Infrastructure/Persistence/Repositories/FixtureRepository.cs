using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class FixtureRepository : IFixtureRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public FixtureRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Fixture?> FindByIdAsync(long id) =>
        await _dbContext.Fixtures
            .AsNoTracking()
            .SingleOrDefaultAsync(fixture => !fixture.IsDeleted &&
                                             fixture.Id == id);

    public async Task CreateAsync(Fixture fixture) =>
        await _dbContext.Fixtures.AddAsync(fixture);
}