using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class FixtureRepository : IFixtureRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public FixtureRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Fixture fixture) =>
        await _dbContext.Fixtures.AddAsync(fixture);
}