﻿using CoachBoard.Domain.Entities;

namespace CoachBoard.Domain.Repositories;
public interface IUserRepository
{
    Task<bool> VerifyIfEmailIsRegisteredAsync(string email, CancellationToken cancellationToken = default);
    void Create(User user);
}