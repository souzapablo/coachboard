using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Players.Commands.Delete;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Players.Commands;

public class DeletePlayerCommandTests
{
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when player is not found")]
    public async Task ShouldThrowExceptionWhenPlayerIsNotFound()
    {
        // Arrange
        var command = new DeletePlayerCommand(1L);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Player>>()
            .WithMessage("Player with id [1] not found");
    }

    [Fact(DisplayName = "Should change IsDeleted to true")]
    public async Task ShouldChangeIsDeletedToTrue()
    {
        // Arrange
        var player = PlayerMock.Generate;
        var command = new DeletePlayerCommand(player.Id);
        var commandHandler = GenerateCommandHandler;
        _playerRepositoryMock.Setup(x => x.FindByIdAsync(player.Id))
            .ReturnsAsync(player);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _playerRepositoryMock.Verify(x => x.Update(player), Times.Once);
        _playerRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        player.IsDeleted.Should().BeTrue();
    }

    private DeletePlayerCommandHandler GenerateCommandHandler =>
        new(_playerRepositoryMock.Object);
}