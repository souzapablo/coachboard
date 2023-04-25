using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Queries.FindAll;

public record FindAllCareersQuery(
    string? CareerManager,
    int Page = 1) : IRequest<PaginationResult<CareerView>>;