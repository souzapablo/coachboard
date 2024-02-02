﻿namespace CoachBoard.Domain.Repositories;
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
