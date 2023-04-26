using CoachBoard.Application.ViewModels.Teams;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Teams.Queries.FindAll;

public class FindAllTeamsQueryHandler : IRequestHandler<FindAllTeamsQuery, PaginationResult<TeamView>>
{
    private readonly ITeamRepository _teamRepository;

    public FindAllTeamsQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<PaginationResult<TeamView>> Handle(FindAllTeamsQuery request, CancellationToken cancellationToken)
    {
        var teams = await _teamRepository.FindAllAsync(request.Name, request.Page);
        var teamViews = new PaginationResult<TeamView>
        {
            TotalPages = teams.TotalPages,
            ItemsCount = teams.ItemsCount,
            Page = teams.Page,
            PageSize = teams.PageSize,
            Data = teams.Data.Select(TeamView.Map).ToList()
        };
        return teamViews;
    }
}
