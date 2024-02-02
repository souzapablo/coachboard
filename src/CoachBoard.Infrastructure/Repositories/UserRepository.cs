﻿using CoachBoard.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Repositories;
public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<bool> VerifyIfEmailIsRegisteredAsync(string email, CancellationToken cancellationToken) =>
        await context
            .Users
            .AnyAsync(user => user.Email.ToLower().Equals(email.ToLower()), cancellationToken: cancellationToken);
}
