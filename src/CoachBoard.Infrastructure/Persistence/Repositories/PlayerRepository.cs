using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public PlayerRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers) =>
        await _dbContext.Players.AddRangeAsync(initialTeamInitialPlayers);
}