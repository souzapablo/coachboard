using CoachBoard.Application.Features.Users.Commands.Create;

namespace CoachBoard.UnitTests.Features.Users.Commands;
public class CreateUserCommandHandlerTests
{
    private static readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private static readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CreateUserCommandHandler _commandHandler = new(_userRepository, _unitOfWork);

    [Fact(DisplayName = "CommandHandler should return error when e-mail is already registered")]
    public async Task CommandHandler_Should_ReturnError_When_EmailIsAlreadyRegistered()
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

    [Fact(DisplayName = "CommandHandler should create a new user when command is valid")]
    public async Task CommandHandler_Should_CreateNewUser_When_CommandIsValid()
    {
        // Arrange
        var command = new CreateUserCommand("test", "teste@email.com", "swordfish");
        _userRepository.VerifyIfEmailIsRegisteredAsync(Arg.Any<string>())
            .Returns(false);

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        await _unitOfWork.Received(1).SaveChangesAsync();
        _userRepository.Received(1).Create(Arg.Any<User>());
    }
}
