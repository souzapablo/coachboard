using CoachBoard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure;
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
