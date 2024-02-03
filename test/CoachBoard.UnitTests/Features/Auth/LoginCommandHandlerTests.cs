using CoachBoard.Application.Features.Auth.Commands.Login;
using CoachBoard.Domain.Helpers;

namespace CoachBoard.UnitTests.Features.Auth;
using BCrypt.Net;

public class LoginCommandHandlerTests
{
    private static readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private static readonly IJwtProvider _jwtProvider = Substitute.For<IJwtProvider>();
    private readonly LoginCommandHandler _commandHandler = new(_userRepository, _jwtProvider);

    [Fact(DisplayName = "CommandHandler should return error when username is invalid")]
    public async Task CommandHandler_Should_ReturnError_When_UsernameIsInvalid()
    {
        // Arrange
        var command = new LoginCommand("teste", "swordfish");

        _userRepository.GetByUsername(Arg.Any<string>())
            .ReturnsNull();

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Message.Should().Be("The credentials provided are invalid.");
    }

    [Fact(DisplayName = "CommandHandler should return error when password is invalid")]
    public async Task CommandHandler_Should_ReturnError_When_PasswordIsInvalid()
    {
        // Arrange
        var command = new LoginCommand("teste", "shark");

        var passwordHash = BCrypt.HashPassword("swordfish");

        var user = new User(Guid.NewGuid(), "swordfish", "test@email.com", passwordHash);

        _userRepository.GetByUsername(Arg.Any<string>())
            .Returns(user);

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Message.Should().Be("The credentials provided are invalid.");
    }

    [Fact(DisplayName = "CommandHandler should return token when credentials are valid")]
    public async Task CommandHandler_Should_ReturnToken_When_CredentialsAreValid()
    {
        // Arrange
        var command = new LoginCommand("teste", "swordfish");

        var passwordHash = BCrypt.HashPassword("swordfish");

        var user = new User(Guid.NewGuid(), "swordfish", "test@email.com", passwordHash);

        _userRepository.GetByUsername(Arg.Any<string>())
            .Returns(user);

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
    }
}
