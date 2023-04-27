using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Teams.Commands.Delete;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Teams.Commands;

public class DeleteTeamCommandTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when team is not found")]
    public async Task ShouldThrowExceptionWhenTeamIsNotFound()
    {
        // Arrange
        var command = new DeleteTeamCommand(1L);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _teamRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Team>()), Times.Never);
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Team>>()
            .WithMessage("Team with id [1] not found");
    }


    [Fact(DisplayName = "Should change IsDeleted to true")]
    public async Task ShouldChangeIsDeletedToTrue()
    {
        // Arrange
        var team = TeamMock.Generate;
        var command = new DeleteTeamCommand(team.Id);
        var commandHandler = GenerateCommandHandler;
        _teamRepositoryMock.Setup(x => x.FindByIdAsync(team.Id))
            .ReturnsAsync(team);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _teamRepositoryMock.Verify(x => x.UpdateAsync(team), Times.Once);
        team.IsDeleted.Should().BeTrue();
    }

    private DeleteTeamCommandHandler GenerateCommandHandler =>
        new(_teamRepositoryMock.Object);
}