using CoachBoard.Application.Features.Users.Commands.Delete;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace CoachBoard.UnitTests.Features.Users.Commands;
public class DeleteUserCommandHandlerTests
{
    private static readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private static readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly DeleteUserCommandHandler _commandHandler = new(_userRepository, _unitOfWork);

    [Fact(DisplayName = "CommandHandler should return error when user not found")]
    public async Task CommandHandler_Should_ReturnError_When_UserNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new DeleteUserCommand(id);

        _userRepository.GetByIdAsync(id)
            .ReturnsNull();

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Message.Should().Be($"User with id {id} not found.");
    }

    [Fact(DisplayName = "CommandHandler should delete user when user is valid")]
    public async Task CommandHandler_Should_DeleteUser_When_UserIsValid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new DeleteUserCommand(id);
        var user = new User(id, "test", "test@email.com", "swordfish");

        _userRepository.GetByIdAsync(id)
            .Returns(user);

        // Act
        var testResult = await _commandHandler.Handle(command, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        user.IsDeleted.Should().BeTrue();
        _userRepository.Received(1).Update(user);
        await _unitOfWork.Received(1).SaveChangesAsync();
    }
}
