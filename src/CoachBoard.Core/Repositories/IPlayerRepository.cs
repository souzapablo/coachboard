using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IPlayerRepository
{
    Task<Player?> FindByIdAsync(long id);
    Task<List<Player>> FindSquadAsync(List<long> playersIds);
    void UpdateSquadAsync(List<Player> players);
    Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers);
}