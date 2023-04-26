using CoachBoard.Application.ViewModels.Teams;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Teams.Queries.FindAll;

public record FindAllTeamsQuery(
    string? Name,
    int Page = 1) : IRequest<PaginationResult<TeamView>>;