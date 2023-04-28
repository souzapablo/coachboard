using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Repositories;

public interface IAssistRepository
{
    Task Create(Assist assist);
}