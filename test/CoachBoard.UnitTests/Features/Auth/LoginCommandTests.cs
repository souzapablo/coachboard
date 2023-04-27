using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Auth.Login;
using CoachBoard.Application.ViewModels.Auth;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Extensions;
using CoachBoard.Core.Repositories;
using CoachBoard.Core.Services;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Auth;

public class LoginCommandTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<IAuthService> _authServiceMock = new();

    [Fact(DisplayName = "Should throw exception if credentials are invalid")]
    public async Task ShouldThrowExceptionIfLoginDataIsInvalid()
    {
        // Arrange
        var command = new LoginCommand("test", "test");
        var commandHandler = GenerateCommandHandler;

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<InvalidLoginException>()
            .WithMessage("Invalid username or password");
    }

    [Fact(DisplayName = "Should return token if credentials are valid")]
    public async Task ShouldReturnTokenIfCredentialsAreValid()
    {
        // Arrange
        var user = UserMock.Generate;
        var command = new LoginCommand(user.Nickname, user.Password);
        var commandHandler = GenerateCommandHandler;
        _authServiceMock.Setup(x => x.ComputeSha256Hash(user.Password))
            .Returns("HashPassword");
        _authServiceMock.Setup(x => x.GenerateJwtToken(user.Nickname, EnumExtensions.GetDescription(user.Role)))
            .Returns("GenerateToken");
        _userRepositoryMock.Setup(x => x.FindByNicknameAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        sut.Should().BeOfType<LoginView>();
        sut.Token.Should().NotBeNull();
    }

    private LoginCommandHandler GenerateCommandHandler =>
        new(_userRepositoryMock.Object,
            _authServiceMock.Object);
}