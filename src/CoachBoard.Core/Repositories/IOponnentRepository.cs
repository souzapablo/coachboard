using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Core.Repositories;

public interface IOpponentRepository
{
    Task<PaginationResult<Opponent>> FindAllAsync(string? name, int page);
    Task<Opponent?> FindByIdAsync(long id);
    Task CreateAsync(Opponent opponent);
    void Update(Opponent opponent);
    Task SaveChangesAsync();
}