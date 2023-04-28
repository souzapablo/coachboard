using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IFixtureRepository
{
    Task Create(Fixture fixture);
}