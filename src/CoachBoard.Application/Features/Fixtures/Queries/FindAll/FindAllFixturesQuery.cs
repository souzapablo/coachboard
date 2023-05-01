using CoachBoard.Application.ViewModels.Fixtures;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Queries.FindAll;

public record FindAllFixturesQuery(
    int Page = 1) : IRequest<PaginationResult<FixtureView>>; 