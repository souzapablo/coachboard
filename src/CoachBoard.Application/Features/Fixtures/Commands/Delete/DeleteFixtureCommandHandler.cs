using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Commands.Delete;

public class DeleteFixtureCommandHandler : IRequestHandler<DeleteFixtureCommand, Unit>
{
    private readonly IFixtureRepository _fixtureRepository;

    public DeleteFixtureCommandHandler(IFixtureRepository fixtureRepository)
    {
        _fixtureRepository = fixtureRepository;
    }

    public async Task<Unit> Handle(DeleteFixtureCommand request, CancellationToken cancellationToken)
    {
        var fixture = await _fixtureRepository.FindByIdAsync(request.Id);

        if (fixture is null)
            throw new EntityNotFoundException<Fixture>(request.Id);
        
        fixture.Delete();

        _fixtureRepository.Update(fixture);

        await _fixtureRepository.SaveChangesAsync();

        return Unit.Value;
    }
}