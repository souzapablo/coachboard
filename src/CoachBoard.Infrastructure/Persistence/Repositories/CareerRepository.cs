using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class CareerRepository : ICareerRepository
{
    private readonly CoachBoardDbContext _dbContext;
    private const int PageSize = 10;

    public CareerRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<PaginationResult<Career>> FindAllAsync(string? managerName, int page)
    {
        var careers = _dbContext.Careers
            .Where(career => !career.IsDeleted);

        if (!string.IsNullOrWhiteSpace(managerName))
            careers = careers.Where(career =>
                career.ManagerName.ToLower()
                    .Contains(managerName.ToLower()));

        return careers.GetPaged(page, PageSize);
    }

    public async Task<Career?> FindByIdAsync(long id) =>
        await _dbContext.Careers
            .Where(career => !career.IsDeleted)
            .Include(career => career.Teams)
            .AsNoTracking()
            .SingleOrDefaultAsync(career => career.Id == id);

    public async Task CreateAsync(Career career)
    {
        await _dbContext.Careers.AddAsync(career);
        await _dbContext.SaveChangesAsync();
    }
}