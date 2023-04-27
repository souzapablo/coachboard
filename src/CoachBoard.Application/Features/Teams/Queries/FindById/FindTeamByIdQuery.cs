using CoachBoard.Application.ViewModels.Teams;
using MediatR;

namespace CoachBoard.Application.Features.Teams.Queries.FindById;

public record FindTeamByIdQuery(
    long Id) : IRequest<TeamDetailsView>;