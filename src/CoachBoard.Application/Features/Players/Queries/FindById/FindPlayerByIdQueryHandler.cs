using CoachBoard.Application.ViewModels.Players;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Players.Queries.FindById;

public class FindPlayerByIdQueryHandler : IRequestHandler<FindPlayerByIdQuery, PlayerDetailsView>
{
    private readonly IPlayerRepository _playerRepository;

    public FindPlayerByIdQueryHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PlayerDetailsView> Handle(FindPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.FindByIdAsync(request.Id);

        if (player is null)
            throw new EntityNotFoundException<Player>(request.Id);

        return PlayerDetailsView.Map(player);
    }
}