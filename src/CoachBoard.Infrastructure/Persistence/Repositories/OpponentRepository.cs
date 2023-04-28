using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class OpponentRepository : IOpponentRepository
{
    private readonly CoachBoardDbContext _dbContext;
    private const int PageSize = 10;

    public OpponentRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PaginationResult<Opponent>> FindAllAsync(string? name, int page)
    {
        var opponents = _dbContext.Opponents
            .AsNoTracking()
            .Where(opponent => !opponent.IsDeleted);

        if (!string.IsNullOrWhiteSpace(name))
            opponents = opponents.Where(opponent => opponent.Name.ToLower()
                .Contains(name.ToLower()));

        return opponents.GetPaged(page, PageSize);
    }

    public async Task<Opponent?> FindByIdAsync(long id) =>
        await _dbContext.Opponents
            .AsNoTracking()
            .SingleOrDefaultAsync(opponent => !opponent.IsDeleted &&
                                              opponent.Id == id);

    public async Task CreateAsync(Opponent opponent) =>
        await _dbContext.Opponents
            .AddAsync(opponent);

    public void Update(Opponent opponent) =>
        _dbContext.Opponents.Update(opponent);

    public Task SaveChangesAsync() =>
        _dbContext.SaveChangesAsync();
}