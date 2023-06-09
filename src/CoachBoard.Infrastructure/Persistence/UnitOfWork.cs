using CoachBoard.Core.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoachBoard.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private IDbContextTransaction _transaction = null!;
    private readonly CoachBoardDbContext _dbContext;

    public UnitOfWork(
        CoachBoardDbContext dbContext,
        IUserRepository users,
        ICareerRepository careers,
        ITeamRepository teams,
        IPlayerRepository players,
        IFixtureRepository fixtures,
        IOpponentRepository opponents,
        IAssistRepository assists,
        IGoalRepository goals)
    {
        _dbContext = dbContext;
        Users = users;
        Careers = careers;
        Players = players;
        Teams = teams;
        Fixtures = fixtures;
        Opponents = opponents;
        Assists = assists;
        Goals = goals;
    }

    public IUserRepository Users { get; }
    public ICareerRepository Careers { get; }
    public ITeamRepository Teams { get; }
    public IPlayerRepository Players { get; }
    public IFixtureRepository Fixtures { get; }
    public IOpponentRepository Opponents { get; }
    public IAssistRepository Assists { get; }
    public IGoalRepository Goals { get; }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database
            .BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception)
        {
            await _transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<int> CompleteAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}