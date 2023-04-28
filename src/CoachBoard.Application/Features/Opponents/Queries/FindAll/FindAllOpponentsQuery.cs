using CoachBoard.Application.ViewModels.Opponents;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Opponents.Queries.FindAll;

public record FindAllOpponentsQuery(
    string? Name,
    int Page = 1) : IRequest<PaginationResult<OpponentView>>;