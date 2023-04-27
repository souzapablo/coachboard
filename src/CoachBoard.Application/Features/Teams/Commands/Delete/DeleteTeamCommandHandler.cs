using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Teams.Commands.Delete;

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Unit>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Unit> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.FindByIdAsync(request.Id);

        if (team is null)
            throw new EntityNotFoundException<Team>(request.Id);
        
        team.Delete();

        await _teamRepository.UpdateAsync(team);

        return Unit.Value;
    }
}