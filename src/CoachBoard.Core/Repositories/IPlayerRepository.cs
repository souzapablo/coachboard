using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IPlayerRepository
{
    Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers);
}