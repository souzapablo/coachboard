using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Users.Commands.Delete;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Users.Commands;

public class DeleteUserCommandTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when user is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var command = new DeleteUserCommand(1L);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<User>>()
            .WithMessage("User with id [1] not found");
    }

    [Fact(DisplayName = "Should change IsDeleted to true")]
    public async Task ShouldChangeIsDeletedToTrue()
    {
        // Arrange
        var user = UserMock.Generate;
        var command = new DeleteUserCommand(user.Id);
        var commandHandler = GenerateCommandHandler;
        _userRepositoryMock.Setup(x => x.FindByIdAsync(user.Id))
            .ReturnsAsync(user);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
        user.IsDeleted.Should().BeTrue();
    }

    private DeleteUserCommandHandler GenerateCommandHandler =>
        new(_userRepositoryMock.Object);
}