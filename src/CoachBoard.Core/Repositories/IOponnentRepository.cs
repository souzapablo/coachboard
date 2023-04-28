using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IOpponentRepository
{
    Task<Opponent?> FindByIdAsync(long id);
}