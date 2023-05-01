using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Fixtures.Commands.Delete;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Fixtures.Commands;

public class DeleteFixtureCommandTests
{
    private readonly Mock<IFixtureRepository> _fixtureRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when fixture is not found")]
    public async Task ShouldThrowExceptionWhenOpponentIsNotFound()
    {
        // Arrange
        var command = new DeleteFixtureCommand(1L);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Fixture>>()
            .WithMessage("Fixture with id [1] not found");
    }

    [Fact(DisplayName = "Should change IsDeleted to true")]
    public async Task ShouldChangeIsDeletedToTrue()
    {
        // Arrange
        var fixture = FixtureMock.Generate;
        var command = new DeleteFixtureCommand(fixture.Id);
        var commandHandler = GenerateCommandHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixture.Id))
            .ReturnsAsync(fixture);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _fixtureRepositoryMock.Verify(x => x.Update(fixture), Times.Once);
        _fixtureRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        fixture.IsDeleted.Should().BeTrue();
    }

    private DeleteFixtureCommandHandler GenerateCommandHandler =>
        new(_fixtureRepositoryMock.Object);
}