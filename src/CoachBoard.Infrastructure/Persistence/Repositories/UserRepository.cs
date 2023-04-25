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
        var users = _dbContext.Users
            .Where(user => !user.IsDeleted);

        if (!string.IsNullOrWhiteSpace(nickname))
            users = users.Where(user =>
                user.Nickname.ToLower()
                    .Contains(nickname.ToLower()));

        return users.GetPaged(page, PageSize);
    }

    public async Task<User?> FindByIdAsync(long id) =>
        await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id &&
                                         !user.IsDeleted);

    public async Task CreateAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> FindByEmailAsync(string email) =>
        await _dbContext.Users
            .AnyAsync(user => user.Email.Equals(email.ToLower()));

    public async Task<User?> FindByNicknameAndPasswordAsync(string nickname, string password) =>
        await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Nickname.Equals(nickname) &&
                                         user.Password.Equals(password) &&
                                         !user.IsDeleted);
}