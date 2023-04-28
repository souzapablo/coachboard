using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Opponents.Commands.Delete;

public class DeleteOpponentCommandHandler : IRequestHandler<DeleteOpponentCommand, Unit>
{
    private readonly IOpponentRepository _opponentRepository;

    public DeleteOpponentCommandHandler(IOpponentRepository opponentRepository)
    {
        _opponentRepository = opponentRepository;
    }

    public async Task<Unit> Handle(DeleteOpponentCommand request, CancellationToken cancellationToken)
    {
        var opponent = await _opponentRepository.FindByIdAsync(request.Id);

        if (opponent is null)
            throw new EntityNotFoundException<Opponent>(request.Id);
        
        opponent.Delete();

        _opponentRepository.Update(opponent);

        await _opponentRepository.SaveChangesAsync();

        return Unit.Value;
    }
}