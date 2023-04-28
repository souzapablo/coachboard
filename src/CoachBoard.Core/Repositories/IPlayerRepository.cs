using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Core.Repositories;

public interface IPlayerRepository
{
    Task<PaginationResult<Player>> FindAllAsync(string? name, int page);
    Task<Player?> FindByIdAsync(long id);
    Task<List<Player>> FindSquadAsync(List<long> playersIds);
    void UpdateSquadAsync(IEnumerable<Player> players);
    Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers);
    Task CreateAsync(Player player);
    Task SaveChangesAsync();
    void Update(Player player);
}