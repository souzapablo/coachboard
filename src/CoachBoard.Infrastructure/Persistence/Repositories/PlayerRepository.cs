using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly CoachBoardDbContext _dbContext;
    private const int PageSize = 10;

    public PlayerRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaginationResult<Player>> FindAllAsync(string? name, int page)
    {
        var players = _dbContext.Players
            .Where(player => !player.IsDeleted);

        if (!string.IsNullOrWhiteSpace(name))
            players = players.Where(player => player.Name.ToLower()
                .Contains(name));

        return await players.GetPaged(page, PageSize);
    }

    public async Task<Player?> FindByIdAsync(long id) =>
        await _dbContext.Players
            .Include(player => player.Fixtures)
            .Include(player => player.Goals)
            .Include(player => player.Assists)
            .AsNoTracking()
            .SingleOrDefaultAsync(player => !player.IsDeleted &&
                                            player.Id == id);

    public async Task<List<Player>> FindSquadAsync(List<long> playersIds) =>
        await _dbContext.Players
            .AsNoTracking()
            .Where(player => !player.IsDeleted &&
                             playersIds.Contains(player.Id))
            .ToListAsync();

    public void UpdateSquadAsync(IEnumerable<Player> players) =>
        _dbContext.Players.UpdateRange(players);


    public async Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers) =>
        await _dbContext.Players.AddRangeAsync(initialTeamInitialPlayers);

    public async Task CreateAsync(Player player) =>
        await _dbContext.Players.AddAsync(player);

    public async Task SaveChangesAsync() =>
        await _dbContext.SaveChangesAsync();

    public void Update(Player player) =>
        _dbContext.Players.Update(player);
}