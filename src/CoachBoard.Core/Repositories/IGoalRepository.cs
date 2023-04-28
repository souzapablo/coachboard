using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IGoalRepository
{
    Task Create(Goal goal);
}