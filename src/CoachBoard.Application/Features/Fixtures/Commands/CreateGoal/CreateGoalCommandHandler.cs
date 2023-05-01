using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Infrastructure.Persistence;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Commands.CreateGoal;

public class CreateGoalCommandHandler : IRequestHandler<CreateGoalCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateGoalCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var fixture = await _unitOfWork.Fixtures.FindByIdAsync(request.FixtureId);
        
        if (fixture is null)
            throw new EntityNotFoundException<Fixture>(request.FixtureId);

        await _unitOfWork.BeginTransactionAsync();

        if (!request.PlayerScoredId.HasValue)
        {
            var ownGoal = new Goal(request.FixtureId);
            await _unitOfWork.Goals.CreateAsync(ownGoal);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();
            return fixture.Id;
        }

        var player = await _unitOfWork.Players.FindByIdAsync(request.PlayerScoredId.Value);

        if (player is null)
            throw new EntityNotFoundException<Player>(request.PlayerScoredId.Value);

        var goal = new Goal(fixture.Id, player.Id);
        
        await _unitOfWork.Goals.CreateAsync(goal);

        await _unitOfWork.CompleteAsync();

        if (request.PlayerAssistedId is not null)
        {
            var playerAssisted = await _unitOfWork.Players.FindByIdAsync(request.PlayerAssistedId.Value);

            if (playerAssisted is null)
                throw new EntityNotFoundException<Player>(request.PlayerAssistedId.Value);
            
            var assist = new Assist(fixture.Id, goal.Id, playerAssisted.Id);

            await _unitOfWork.Assists.Create(assist);

            await _unitOfWork.CompleteAsync();
        }

        await _unitOfWork.CommitAsync();

        return goal.Id;
    }
}