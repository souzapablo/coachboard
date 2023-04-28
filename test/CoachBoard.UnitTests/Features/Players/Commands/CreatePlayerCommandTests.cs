using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Players.Commands.Create;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Players.Commands;

public class CreatePlayerCommandTests
{
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
    private readonly Mock<ITeamRepository> _teamRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when team is not found")]
    public async Task ShouldThrowExceptionWhenTeamIsNotFound()
    {
        // Arrange
        var command = new CreatePlayerCommand(1L,
            "Test Player",
            DateTime.Now.AddDays(-15),
            DateTime.Now.AddYears(-15),
            80,
            PlayerPosition.Cf,
            15,
            PlayerStatus.InSquad);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Team>>()
            .WithMessage("Team with id [1] not found");
    }

    [Fact(DisplayName = "Valid command should create a new player")]
    public async Task ValidCommandShouldCreateCNewOpponent()
    {
        // Arrange
        var team = TeamMock.Generate;
        var command = new CreatePlayerCommand(team.Id,
            "Test Career",
            DateTime.Now.AddDays(-15),
            DateTime.Now.AddYears(-15),
            80,
            PlayerPosition.Cf,
            15,
            PlayerStatus.InSquad);
        var commandHandler = GenerateCommandHandler;
        _teamRepositoryMock.Setup(x => x.FindByIdAsync(team.Id))
            .ReturnsAsync(team);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _playerRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Player>()), Times.Once);
        sut.Should().BeOfType(typeof(long));
    }

    private CreatePlayerCommandHandler GenerateCommandHandler =>
        new(_playerRepositoryMock.Object,
            _teamRepositoryMock.Object);
}