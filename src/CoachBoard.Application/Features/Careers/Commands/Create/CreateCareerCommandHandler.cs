using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Extensions;
using CoachBoard.Infrastructure.Persistence;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler : IRequestHandler<CreateCareerCommand, long>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCareerCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FindByIdAsync(request.UserId);

        if (user is null)
            throw new EntityNotFoundException<User>(request.UserId);

        var career = new Career(user.Id, request.ManagerName);

        await _unitOfWork.BeginTransactionAsync();

        await _unitOfWork.Careers.CreateAsync(career);

        await _unitOfWork.CompleteAsync();

        var initialTeam = InitialTeamExtensions.GenerateInitialTeam(request.TeamName);

        if (initialTeam is not null)
        {
            var team = new Team(career.Id, initialTeam.Name, initialTeam.Stadium);

            await _unitOfWork.Teams.CreateAsync(team);

            await _unitOfWork.CompleteAsync();

            var players = initialTeam.InitialPlayers
                .Select(player => new Player(
                    team.Id,
                    player.Name,
                    player.BirthDate,
                    player.JoinedDate,
                    player.Overall,
                    player.KitNumber,
                    player.Position,
                    player.Status))
                .ToList();

            await _unitOfWork.Players.CreateAsync(players);

            await _unitOfWork.CompleteAsync();
        }

        await _unitOfWork.CommitAsync();

        return career.Id;
    }
}