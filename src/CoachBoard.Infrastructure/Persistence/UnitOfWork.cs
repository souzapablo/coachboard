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
        IPlayerRepository players)
    {
        _dbContext = dbContext;
        Users = users;
        Careers = careers;
        Players = players;
        Teams = teams;
    }

    public IUserRepository Users { get; }
    public ICareerRepository Careers { get; }
    public ITeamRepository Teams { get; }
    public IPlayerRepository Players { get; }

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