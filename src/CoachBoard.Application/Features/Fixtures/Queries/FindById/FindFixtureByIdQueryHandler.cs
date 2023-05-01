using CoachBoard.Application.ViewModels.Fixtures;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Queries.FindById;

public class FindFixtureByIdQueryHandler : IRequestHandler<FindFixtureByIdQuery, FixtureDetailsView>
{
    private readonly IFixtureRepository _fixtureRepository;

    public FindFixtureByIdQueryHandler(IFixtureRepository fixtureRepository)
    {
        _fixtureRepository = fixtureRepository;
    }

    public async Task<FixtureDetailsView> Handle(FindFixtureByIdQuery request, CancellationToken cancellationToken)
    {
        var fixture = await _fixtureRepository.FindByIdAsync(request.Id,
            fixture => fixture.Goals,
            fixture => fixture.LineUp,
            fixture => fixture.Assists);

        if (fixture is null)
            throw new EntityNotFoundException<Fixture>(request.Id);

        return FixtureDetailsView.Map(fixture);
    }
}