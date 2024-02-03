using CoachBoard.Application.Features.Users.Queries.GetById;

namespace CoachBoard.UnitTests.Features.Users.Queries;
public class GetUserByIdQueryHandlerTests
{
    private static readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly GetUserByIdQueryHandler _querydHandler = new(_userRepository);

    [Fact(DisplayName = "QueryHandler should return error when user is not found")]
    public async Task QueryHandler_Should_ReturnError_When_UserIsNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var query = new GetUserByIdQuery(id);

        _userRepository.GetByIdAsync(Arg.Any<Guid>())
            .ReturnsNull();

        // Act 
        var testResult = await _querydHandler.Handle(query, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeFalse();
        testResult.Error?.Message.Should().Be($"User with id {id} not found.");
    }

    [Fact(DisplayName = "QueryHandler should return user response when user is found")]
    public async Task QueryHandler_Should_UserReponse_When_UserIsFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var query = new GetUserByIdQuery(id);

        var user = new User(id, "test", "test@email.com", "swordfish");

        _userRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(user);

        // Act 
        var testResult = await _querydHandler.Handle(query, new CancellationToken());

        // Assert
        testResult.IsSuccess.Should().BeTrue();
        testResult.Data.Should().NotBeNull();
    }
}
