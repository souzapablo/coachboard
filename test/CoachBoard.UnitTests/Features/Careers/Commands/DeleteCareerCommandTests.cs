using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Careers.Commands.Delete;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Careers.Commands;

public class DeleteCareerCommandTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when career is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var command = new DeleteCareerCommand(1L);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Career>>()
            .WithMessage("Career with id [1] not found");
    }

    [Fact(DisplayName = "Should change IsDeleted to true")]
    public async Task ShouldChangeIsDeletedToTrue()
    {
        // Arrange
        var career = CareerMock.Generate;
        var command = new DeleteCareerCommand(career.Id);
        var commandHandler = GenerateCommandHandler;
        _careerRepositoryMock.Setup(x => x.FindByIdAsync(career.Id))
            .ReturnsAsync(career);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _careerRepositoryMock.Verify(x => x.UpdateAsync(career), Times.Once);
        career.IsDeleted.Should().BeTrue();
    }

    private DeleteCareerCommandHandler GenerateCommandHandler =>
        new(_careerRepositoryMock.Object);
}