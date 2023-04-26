using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Users.Queries.FindById;
using CoachBoard.Application.ViewModels.Users;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Users.Queries;

public class FindUserByIdQueryTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when user is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var query = new FindUserByIdQuery(1L);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<User>>()
            .WithMessage("User with id [1] not found");
    }

    [Fact(DisplayName = "Should return UserDetailsView if user is found")]
    public async Task ShouldReturnUserDetailsViewIfUserIsFound()
    {
        // Arrange
        var queryHandler = GenerateQueryHandler;
        var user = UserMock.Generate;
        var query = new FindUserByIdQuery(user.Id);
        _userRepositoryMock.Setup(x => x.FindByIdAsync(user.Id))
            .ReturnsAsync(user);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<UserDetailsView>();
        sut.Id.Should().Be(user.Id);
        sut.Nickname.Should().Be(user.Nickname);
        sut.Email.Should().Be(user.Email);
    }

    private FindUserByIdQueryHandler GenerateQueryHandler =>
        new(_userRepositoryMock.Object);
}