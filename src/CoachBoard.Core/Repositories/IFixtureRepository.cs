using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IFixtureRepository
{
    Task<Fixture?> FindByIdAsync(long id);
    Task CreateAsync(Fixture fixture);
}