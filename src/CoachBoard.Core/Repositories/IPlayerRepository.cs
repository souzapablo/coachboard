using CoachBoard.Core.Entities;

namespace CoachBoard.Application.Repositories;

public interface IPlayerRepository
{
    Task CreateAsync(IEnumerable<Player> initialTeamInitialPlayers);
}