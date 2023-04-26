using CoachBoard.Core.Entities;

namespace CoachBoard.Application.Repositories;

public interface ITeamRepository
{
    Task CreateAsync(Team team);
}