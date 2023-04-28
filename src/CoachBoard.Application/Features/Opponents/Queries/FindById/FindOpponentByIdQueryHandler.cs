using CoachBoard.Application.ViewModels.Opponents;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Opponents.Queries.FindById;

public class FindOpponentByIdQueryHandler : IRequestHandler<FindOpponentByIdQuery, OpponentDetailsView>
{
    private readonly IOpponentRepository _opponentRepository;

    public FindOpponentByIdQueryHandler(IOpponentRepository opponentRepository)
    {
        _opponentRepository = opponentRepository;
    }

    public async Task<OpponentDetailsView> Handle(FindOpponentByIdQuery request, CancellationToken cancellationToken)
    {
        var opponent = await _opponentRepository.FindByIdAsync(request.Id);

        if (opponent is null)
            throw new EntityNotFoundException<Opponent>(request.Id);

        return OpponentDetailsView.Map(opponent);
    }
}