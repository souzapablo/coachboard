using CoachBoard.Application.ViewModels.Fixtures;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Queries.FindAll;

public class FindAllFixturesQueryHandler : IRequestHandler<FindAllFixturesQuery, PaginationResult<FixtureView>>
{
    private readonly IFixtureRepository _fixtureRepository;

    public FindAllFixturesQueryHandler(IFixtureRepository fixtureRepository)
    {
        _fixtureRepository = fixtureRepository;
    }

    public async Task<PaginationResult<FixtureView>> Handle(FindAllFixturesQuery request, CancellationToken cancellationToken)
    {
        var fixture = await _fixtureRepository.FindAllAsync(request.Page);
        var paginatedFixtureView = new PaginationResult<FixtureView>
        {
            ItemsCount = fixture.ItemsCount,
            Page = fixture.Page,
            PageSize = fixture.PageSize,
            TotalPages = fixture.TotalPages,
            Data = fixture.Data.Select(FixtureView.Map).ToList()
        };
        return paginatedFixtureView;
    }
}