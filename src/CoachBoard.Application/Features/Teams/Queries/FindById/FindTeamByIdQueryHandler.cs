using CoachBoard.Application.ViewModels.Teams;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Teams.Queries.FindById;

public class FindTeamByIdQueryHandler : IRequestHandler<FindTeamByIdQuery, TeamDetailsView>
{
    private readonly ITeamRepository _teamRepository;

    public FindTeamByIdQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<TeamDetailsView> Handle(FindTeamByIdQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.FindByIdAsync(request.Id);

        if (team is null)
            throw new EntityNotFoundException<Team>(request.Id);

        return TeamDetailsView.Map(team);
    }
}