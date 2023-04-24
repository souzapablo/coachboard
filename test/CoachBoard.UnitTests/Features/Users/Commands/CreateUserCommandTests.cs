using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Users.Commands;

public class CreateUserCommandTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IAuthService> _authServiceMock = new();

    [Fact(DisplayName = "Taken e-mail should throw exception")]
    public async Task GivenAnAlreadyTakenEmailShouldThrowException()
    {
        // Arrange
        var command = new CreateUserCommand(
            "myName",
            "szpbl@email.com",
            "safePassword");
        var commandHandler = GenerateCommandHandler;
        _userRepositoryMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());
        // Assert
        await sut.Should()
            .ThrowAsync<EmailAlreadyRegisteredException>()
            .WithMessage("E-mail already registered");
    }

    [Fact(DisplayName = "Valid command should create new user")]
    public async Task GivenAValidCommandShouldCreateNewUser()
    {
        // Arrange
        var command = new CreateUserCommand(
            "myName",
            "szpbl@email.com",
            "safePassword");
        var commandHandler = GenerateCommandHandler;
        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());
        // Assert
        _userRepositoryMock.Verify(x =>
            x.FindByEmailAsync(It.IsAny<string>()), Times.Once());
        _userRepositoryMock.Verify(x =>
            x.CreateAsync(It.IsAny<User>()), Times.Once);
        sut.Should()
            .BeOfType(typeof(long));
    }

    private CreateUserCommandHandler GenerateCommandHandler =>
        new CreateUserCommandHandler(
            _userRepositoryMock.Object,
            _authServiceMock.Object);
}