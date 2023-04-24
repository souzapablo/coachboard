using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public UserRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> FindByEmailAsync(string email) =>
        await _dbContext.Users
            .AnyAsync(user => user.Email == email);
}