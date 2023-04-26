using CoachBoard.Application.ViewModels.Careers;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Queries.FindById;

public record FindCareerByIdQuery(
    long Id) : IRequest<CareerDetailsView>;