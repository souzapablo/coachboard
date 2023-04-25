using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Users.Commands.ChangeRole;
using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;
using CoachBoard.Core.Exceptions;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Users.Commands;

public class ChangeRoleCommandTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    
    [Fact(DisplayName = "Should throw exception when user is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var command = new ChangeRoleCommand(1L, Role.Admin);
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<User>>()
            .WithMessage("User with id [1] not found");
    }
    
    [Fact(DisplayName = "Should change role to Admin")]
    public async Task ShouldChangeIsRoleToAdmin()
    {
        // Arrange
        var user = UserMock.Generate;
        var command = new ChangeRoleCommand(user.Id, Role.Admin);
        var commandHandler = GenerateCommandHandler;
        _userRepositoryMock.Setup(x => x.FindByIdAsync(user.Id))
            .ReturnsAsync(user);

        // Act
        await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _userRepositoryMock.Verify(x => x.UpdateAsync(user), Times.Once);
        user.Role.Should().Be(Role.Admin);
    }
    private ChangeRoleCommandHandler GenerateCommandHandler =>
        new(_userRepositoryMock.Object);
}