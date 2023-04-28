using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public PlayerRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Player>> FindSquadAsync(List<long> playersIds) =>
        await _dbContext.Players
            .AsNoTracking()
            .Where(player => !player.IsDeleted &&
                             playersIds.Contains(player.Id))
            .ToListAsync();

    public async Task UpdateSquadAsync(List<Player> players)
    {
        _dbContext.Players.UpdateRange(players);
        await _dbContext.SaveChangesAsync();
    }


    public async Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers) =>
        await _dbContext.Players.AddRangeAsync(initialTeamInitialPlayers);
}