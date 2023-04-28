using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Opponents.Commands.Create;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Opponents.Commands;

public class CreateOpponentCommandTests
{
    private readonly Mock<IOpponentRepository> _opponentRepositoryMock = new();
    private readonly Mock<ICareerRepository> _careerRepositoryMock = new();
    
    [Fact(DisplayName = "Should throw exception when career is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var command = new CreateOpponentCommand(1L, "Test Career", "Test Stadium");
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Career>>()
            .WithMessage("Career with id [1] not found");
    }
    
    [Fact(DisplayName = "Valid command should create a new opponent")]
    public async Task ValidCommandShouldCreateCNewOpponent()
    {
        // Arrange
        var career = CareerMock.Generate;
        var command = new CreateOpponentCommand(career.Id, "Test Opponent", "Test Stadium");
        var commandHandler = GenerateCommandHandler;
        _careerRepositoryMock.Setup(x => x.FindByIdAsync(career.Id))
            .ReturnsAsync(career);
        
        // Act
       var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _opponentRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Opponent>()), Times.Once);
        sut.Should().BeOfType(typeof(long));
    }

    private CreateOpponentCommandHandler GenerateCommandHandler =>
        new(_opponentRepositoryMock.Object,
            _careerRepositoryMock.Object);
}