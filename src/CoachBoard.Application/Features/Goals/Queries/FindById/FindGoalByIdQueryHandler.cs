using CoachBoard.Application.ViewModels.Goals;
using CoachBoard.Application.ViewModels.Players;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Goals.Queries.FindById;

public class FindGoalByIdQueryHandler : IRequestHandler<FindGoalByIdQuery, GoalDetailsView>
{
    private readonly IGoalRepository _goalRepository;

    public FindGoalByIdQueryHandler(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<GoalDetailsView> Handle(FindGoalByIdQuery request, CancellationToken cancellationToken)
    {
        var goal = await _goalRepository.FindByIdAsync(request.Id,
            goal => goal.PlayerScored,
            goal => goal.Assist!.PlayerAssisted);

        if (goal is null)
            throw new EntityNotFoundException<Goal>(request.Id);

        return new GoalDetailsView(
            goal.Id,
            goal.PlayerScored?.Name,
            goal.Assist?.PlayerAssisted.Name);
    }
}