using CoachBoard.Application.ViewModels.Opponents;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Opponents.Queries.FindAll;

public class FindAllOpponentsQueryHandler : IRequestHandler<FindAllOpponentsQuery, PaginationResult<OpponentView>>
{
    private readonly IOpponentRepository _opponentRepository;

    public FindAllOpponentsQueryHandler(IOpponentRepository opponentRepository)
    {
        _opponentRepository = opponentRepository;
    }

    public async Task<PaginationResult<OpponentView>> Handle(FindAllOpponentsQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedOpponents = await _opponentRepository.FindAllAsync(request.Name, request.Page);
        var paginatedOpponentsViews = new PaginationResult<OpponentView>
        {
            ItemsCount = paginatedOpponents.ItemsCount,
            Page = paginatedOpponents.Page,
            PageSize = paginatedOpponents.PageSize,
            TotalPages = paginatedOpponents.TotalPages,
            Data = paginatedOpponents.Data.Select(OpponentView.Map).ToList()
        };
        return paginatedOpponentsViews;
    }
}