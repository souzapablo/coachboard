using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Fixtures.Commands.CreateGoal;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.Infrastructure.Persistence;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Fixtures.Commands;

public class CreateGoalCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IFixtureRepository> _fixtureRepositoryMock = new();
    private readonly Mock<IGoalRepository> _goalRepositoryMock = new();
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();
    private readonly Mock<IAssistRepository> _assistRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception if fixture is not found")]
    public async Task ShouldThrowExceptionIfFixtureIsNotFound()
    {
        // Arrange
        var command = new CreateGoalCommand(1L, null, null);
        var commandHandler = GenerateCommandHandler;
        SetUpUnitOfWork();

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Fixture>>()
            .WithMessage("Fixture with id [1] not found");
    }

    [Fact(DisplayName = "Should create own goal if PlayerScoredId is null")]
    public async Task ShouldCreateOwnGoalIfPlayerScoredIdIsNull()
    {
        // Arrange
        var fixture = FixtureMock.Generate;
        var command = new CreateGoalCommand(fixture.Id, null, null);
        var commandHandler = GenerateCommandHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixture.Id))
            .ReturnsAsync(fixture);
        SetUpUnitOfWork();

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _goalRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Goal>()), Times.Once);
        _playerRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<long>()), Times.Never);
    }

    [Fact(DisplayName = "If PlayerScored is not found should throw exception")]
    public async Task IfPlayerScoredIsNotFoundShouldThrowException()
    {
        // Arrange
        var fixture = FixtureMock.Generate;
        var command = new CreateGoalCommand(fixture.Id, 1L, null);
        var commandHandler = GenerateCommandHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixture.Id))
            .ReturnsAsync(fixture);
        SetUpUnitOfWork();

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Player>>()
            .WithMessage("Player with id [1] not found");
    }

    [Fact(DisplayName = "Should create a goal if PlayerScoredId is not null")]
    public async Task ShouldCreateAGoalIfPlayerScoredIdIsNotNull()
    {
        // Arrange
        var fixture = FixtureMock.Generate;
        var player = PlayerMock.Generate;
        var command = new CreateGoalCommand(fixture.Id, player.Id, null);
        var commandHandler = GenerateCommandHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixture.Id))
            .ReturnsAsync(fixture);
        _playerRepositoryMock.Setup(x => x.FindByIdAsync(player.Id))
            .ReturnsAsync(player);
        SetUpUnitOfWork();

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _goalRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Goal>()), Times.Once);
        _playerRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact(DisplayName = "If PlayerScored is not found should throw exception")]
    public async Task IfPlayerAssistedIsNotFoundShouldThrowException()
    {
        // Arrange
        var fixture = FixtureMock.Generate;
        var playerScored = PlayerMock.Generate;
        var command = new CreateGoalCommand(fixture.Id, playerScored.Id, 1L);
        var commandHandler = GenerateCommandHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixture.Id))
            .ReturnsAsync(fixture);
        _playerRepositoryMock.Setup(x => x.FindByIdAsync(playerScored.Id))
            .ReturnsAsync(playerScored);
        SetUpUnitOfWork();

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Player>>()
            .WithMessage("Player with id [1] not found");
    }

    [Fact(DisplayName = "Should create a goal and a assist if PlayerAssistedId is not null")]
    public async Task ShouldCreateAGoalAndAAssistIfPlayerAssistedIdIsNotNull()
    {
        // Arrange
        var fixture = FixtureMock.Generate;
        var playerScored = PlayerMock.Generate;
        var playerAssisted = PlayerMock.Generate;
        var command = new CreateGoalCommand(fixture.Id, playerScored.Id, playerAssisted.Id);
        var commandHandler = GenerateCommandHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixture.Id))
            .ReturnsAsync(fixture);
        _playerRepositoryMock.Setup(x => x.FindByIdAsync(playerScored.Id))
            .ReturnsAsync(playerScored);
        _playerRepositoryMock.Setup(x => x.FindByIdAsync(playerAssisted.Id))
            .ReturnsAsync(playerAssisted);
        SetUpUnitOfWork();

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _goalRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Goal>()), Times.Once);
        _playerRepositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<long>()), Times.Exactly(2));
    }

    private void SetUpUnitOfWork()
    {
        _unitOfWorkMock.SetupGet(x => x.Fixtures)
            .Returns(_fixtureRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Goals)
            .Returns(_goalRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Players)
            .Returns(_playerRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Assists)
            .Returns(_assistRepositoryMock.Object);
    }

    private CreateGoalCommandHandler GenerateCommandHandler =>
        new(_unitOfWorkMock.Object);
}