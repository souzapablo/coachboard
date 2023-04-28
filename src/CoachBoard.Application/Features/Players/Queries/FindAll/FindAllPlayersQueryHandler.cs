using CoachBoard.Application.ViewModels.Players;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Players.Queries.FindAll;

public class FindAllPlayersQueryHandler : IRequestHandler<FindAllPlayersQuery, PaginationResult<PlayerView>>
{
    private readonly IPlayerRepository _playerRepository;

    public FindAllPlayersQueryHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PaginationResult<PlayerView>> Handle(FindAllPlayersQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedPlayers = await _playerRepository.FindAllAsync(request.Name, request.Page);
        var paginatedPlayersView = new PaginationResult<PlayerView>
        {
            ItemsCount = paginatedPlayers.ItemsCount,
            PageSize = paginatedPlayers.PageSize,
            Page = paginatedPlayers.Page,
            TotalPages = paginatedPlayers.TotalPages,
            Data = paginatedPlayers.Data.Select(PlayerView.Map).ToList()
        };
        return paginatedPlayersView;
    }
}