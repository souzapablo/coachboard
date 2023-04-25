using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CoachBoardDbContext _dbContext;
    private const int PageSize = 10;

    public UserRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PaginationResult<User>> FindAllAsync(string? nickname, int page)
    {
        IQueryable<User> users = _dbContext.Users;

        if (!string.IsNullOrWhiteSpace(nickname))
            users = users.Where(u => u.Nickname == nickname);

        return users.GetPaged(page, PageSize);
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