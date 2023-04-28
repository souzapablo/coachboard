using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Infrastructure.Persistence;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Commands;

public class CreateFixtureCommandHandler : IRequestHandler<CreateFixtureCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateFixtureCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreateFixtureCommand request, CancellationToken cancellationToken)
    {
        var team = await _unitOfWork.Teams.FindByIdAsync(request.TeamId);

        if (team is null)
            throw new EntityNotFoundException<Team>(request.TeamId);

        var opponent = await _unitOfWork.Opponents.FindByIdAsync(request.OpponentId);

        if (opponent is null)
            throw new EntityNotFoundException<Opponent>(request.OpponentId);

        var fixture = new Fixture(
            team.Id,
            opponent.Id,
            request.Location,
            request.Competition);

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Fixtures.Create(fixture);

        await _unitOfWork.CompleteAsync();

        var players = await _unitOfWork.Players.FindSquadAsync(request.PlayersIds);

        players.ForEach(player =>
            player.AddFixture(fixture));

        await _unitOfWork.Players.UpdateSquadAsync(players);

        await _unitOfWork.CompleteAsync();

        await _unitOfWork.CommitAsync();

        return fixture.Id;
    }
}