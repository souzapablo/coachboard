using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace CoachBoard.UnitTests.Features.Users;
public class CreateUserCommandHandlerTests
{
    private static readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private CreateUserCommandHandler _commandHandler = new(_userRepository);

    [Fact(DisplayName = "Command should return error when e-mail is already registered")]
    public async Task Command_Should_ReturnError_When_EmailIsAlreadyRegistered()
    {
        // Arrange
        var command = new CreateUserCommand("test", "teste@email.com", "swordfish");
        _userRepository.VerifyIfEmailIsRegisteredAsync(Arg.Any<string>())
            .Returns(true);

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Message.Should().Be("The email provided has already been registered.");
    }
}
