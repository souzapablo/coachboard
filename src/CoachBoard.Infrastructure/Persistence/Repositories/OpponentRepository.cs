using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class OpponentRepository : IOpponentRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public OpponentRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Opponent?> FindByIdAsync(long id) =>
        await _dbContext.Opponents
            .AsNoTracking()
            .SingleOrDefaultAsync(opponent => !opponent.IsDeleted &&
                                              opponent.Id == id);
}