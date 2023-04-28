using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Fixtures.Commands;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Persistence;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Fixtures.Commands;

public class CreateFixtureCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ITeamRepository> _teamRepositoryMock = new();
    private readonly Mock<IOpponentRepository> _opponentRepositoryMock = new();
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
    private readonly Mock<IFixtureRepository> _fixtureRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception if team is not found")]
    public async Task ShouldThrowExceptionIfTeamIsNotFound()
    {
        // Arrange
        var command = new CreateFixtureCommand(
            1L,
            1L,
            FixtureLocation.Away,
            Competition.ChampionsLeague,
            new List<long>
            {
                1, 2, 3
            });
        var commandHandler = GenerateCommandHandler;
        _unitOfWorkMock.SetupGet(x => x.Teams)
            .Returns(_teamRepositoryMock.Object);

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Team>>()
            .WithMessage("Team with id [1] not found");
    }

    [Fact(DisplayName = "Should throw exception if opponent is not found")]
    public async Task ShouldThrowExceptionIfOpponentIsNotFound()
    {
        // Arrange
        var team = TeamMock.Generate;
        var command = new CreateFixtureCommand(
            team.Id,
            1L,
            FixtureLocation.Away,
            Competition.ChampionsLeague,
            new List<long>
            {
                1, 2, 3
            });
        var commandHandler = GenerateCommandHandler;
        _unitOfWorkMock.SetupGet(x => x.Teams)
            .Returns(_teamRepositoryMock.Object);
        _teamRepositoryMock.Setup(x => x.FindByIdAsync(team.Id))
            .ReturnsAsync(team);
        _unitOfWorkMock.SetupGet(x => x.Opponents)
            .Returns(_opponentRepositoryMock.Object);

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Opponent>>()
            .WithMessage("Opponent with id [1] not found");
    }

    [Fact(DisplayName = "Valid command should create new fixture")]
    public async Task ValidCommandShouldCreateNewFixture()
    {
        // Arrange
        var team = TeamMock.Generate;
        var opponent = OpponentMock.Generate;
        var players = PlayerMock.GenerateMany(3);
        var command = new CreateFixtureCommand(
            team.Id,
            opponent.Id,
            FixtureLocation.Away,
            Competition.ChampionsLeague,
            new List<long>
            {
                1, 2, 3
            });
        var commandHandler = GenerateCommandHandler;

        _unitOfWorkMock.SetupGet(x => x.Teams)
            .Returns(_teamRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Opponents)
            .Returns(_opponentRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Players)
            .Returns(_playerRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Fixtures)
            .Returns(_fixtureRepositoryMock.Object);

        _teamRepositoryMock.Setup(x => x.FindByIdAsync(team.Id))
            .ReturnsAsync(team);
        _opponentRepositoryMock.Setup(x => x.FindByIdAsync(opponent.Id))
            .ReturnsAsync(opponent);
        _playerRepositoryMock.Setup(x => x.FindSquadAsync(command.PlayersIds))
            .ReturnsAsync(players);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _fixtureRepositoryMock.Verify(x => x.Create(It.IsAny<Fixture>()), Times.Once());
        _playerRepositoryMock.Verify(x => x.UpdateSquadAsync(It.IsAny<List<Player>>()), Times.Once);
        sut.Should().BeOfType(typeof(long));
        players.ForEach(player =>
            player.Fixtures.Count.Should().Be(1));
    }

    private CreateFixtureCommandHandler GenerateCommandHandler =>
        new(_unitOfWorkMock.Object);
}