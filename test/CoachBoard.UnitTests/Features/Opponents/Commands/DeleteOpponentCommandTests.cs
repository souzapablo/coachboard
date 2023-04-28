using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Opponents.Commands.Delete;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Opponents.Commands;

public class DeleteOpponentCommandTests
{
    private readonly Mock<IOpponentRepository> _opponentRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when opponent is not found")]
    public async Task ShouldThrowExceptionWhenOpponentIsNotFound()
    {
        // Arrange
        var command = new DeleteOpponentCommand(1L);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Opponent>>()
            .WithMessage("Opponent with id [1] not found");
    }

    [Fact(DisplayName = "Should change IsDeleted to true")]
    public async Task ShouldChangeIsDeletedToTrue()
    {
        // Arrange
        var opponent = OpponentMock.Generate;
        var command = new DeleteOpponentCommand(opponent.Id);
        var commandHandler = GenerateCommandHandler;
        _opponentRepositoryMock.Setup(x => x.FindByIdAsync(opponent.Id))
            .ReturnsAsync(opponent);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _opponentRepositoryMock.Verify(x => x.Update(opponent), Times.Once);
        _opponentRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        opponent.IsDeleted.Should().BeTrue();
    }

    private DeleteOpponentCommandHandler GenerateCommandHandler =>
        new(_opponentRepositoryMock.Object);
}