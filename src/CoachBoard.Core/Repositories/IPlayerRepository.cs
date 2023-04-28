using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IPlayerRepository
{
    Task<List<Player>> FindSquadAsync(List<long> playersIds);
    Task UpdateSquadAsync(List<Player> players);
    Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers);
}