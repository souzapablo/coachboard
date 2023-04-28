using CoachBoard.Core.Entities;
using CoachBoard.Core.Repositories;

namespace CoachBoard.Infrastructure.Persistence.Repositories;

public class AssistRepository : IAssistRepository
{
    private readonly CoachBoardDbContext _dbContext;

    public AssistRepository(CoachBoardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(Assist assist) =>
        await _dbContext.AddAsync(assist);
}