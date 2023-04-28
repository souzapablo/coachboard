using CoachBoard.Core.Repositories;

namespace CoachBoard.Infrastructure.Persistence;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    ICareerRepository Careers { get; }
    ITeamRepository Teams { get; }
    IPlayerRepository Players { get; }
    IFixtureRepository Fixtures { get; }
    IOpponentRepository Opponents { get; }
    Task<int> CompleteAsync();
    Task BeginTransactionAsync();
    Task CommitAsync();
}