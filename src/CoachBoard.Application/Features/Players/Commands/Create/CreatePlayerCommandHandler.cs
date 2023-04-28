using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Players.Commands.Create;

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, long>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ITeamRepository _teamRepository;

    public CreatePlayerCommandHandler(IPlayerRepository playerRepository, ITeamRepository teamRepository)
    {
        _playerRepository = playerRepository;
        _teamRepository = teamRepository;
    }

    public async Task<long> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.FindByIdAsync(request.TeamId);

        if (team is null)
            throw new EntityNotFoundException<Team>(request.TeamId);

        var player = new Player(
            team.Id,
            request.Name,
            request.BirthDate,
            request.JoinedDate,
            request.Overall,
            request.KitNumber,
            request.Position,
            request.Status);

        await _playerRepository.CreateAsync(player);

        await _playerRepository.SaveChangesAsync();

        return player.Id;
    }
}