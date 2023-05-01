using CoachBoard.Application.ViewModels.Fixtures;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Queries.FindById;

public record FindFixtureByIdQuery(
    long Id) : IRequest<FixtureDetailsView>;