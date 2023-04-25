using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;

namespace CoachBoard.Application.Repositories;

public interface ICareerRepository
{
    Task<PaginationResult<Career>> FindAllAsync(string? managerName, int page);
}