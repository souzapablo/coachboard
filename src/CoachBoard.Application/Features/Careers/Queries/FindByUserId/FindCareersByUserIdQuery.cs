using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Queries.FindByUserId;

public record FindCareersByUserIdQuery(
    long UserId,
    int Page) : IRequest<PaginationResult<CareerView>>;