using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Players.Commands.Delete;

public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, Unit>
{
    private readonly IPlayerRepository _playerRepository;

    public DeletePlayerCommandHandler(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _playerRepository.FindByIdAsync(request.Id);

        if (player is null)
            throw new EntityNotFoundException<Player>(request.Id);
        
        player.Delete();
        
        _playerRepository.Update(player);

        await _playerRepository.SaveChangesAsync();

        return Unit.Value;
    }
}