﻿using CoachBoard.Domain.Entities;
using CoachBoard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Repositories;
public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<bool> VerifyIfEmailIsRegisteredAsync(string email, CancellationToken cancellationToken) =>
        await context
            .Users
            .AnyAsync(user => user.Email.ToLower().Equals(email.ToLower()), cancellationToken: cancellationToken);

    public void Create(User user) =>
        context.Users.Add(user);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await context
            .Users
            .Include(user => user.Careers)
            .ThenInclude(career => career.Teams)
            .SingleOrDefaultAsync(user => user.Id == id, cancellationToken: cancellationToken);

    public IQueryable<User> List() =>
         context
            .Users
            .Include(user => user.Careers)
            .ThenInclude(career => career.Teams);
}
