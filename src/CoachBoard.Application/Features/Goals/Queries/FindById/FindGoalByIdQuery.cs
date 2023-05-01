using CoachBoard.Application.ViewModels.Goals;
using MediatR;

namespace CoachBoard.Application.Features.Goals.Queries.FindById;

public record FindGoalByIdQuery(
    long Id) : IRequest<GoalDetailsView>;