using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Users.Queries.FindAll;
using CoachBoard.Application.ViewModels.Users;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Users.Queries;

public class FindAllUsersQueryTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    [Fact(DisplayName = "Should return a page of UserViews")]
    public async Task ShouldReturnAPageOfUserViews()
    {
        // Assert
        var paginatedUsers = new PaginationResult<User>
        {
            Data = UserMock.GenerateMany(3)
        };
        var query = new FindAllUsersQuery(null);
        var queryHandler = GenerateQueryHandler;
        _userRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(paginatedUsers);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Data.Count.Should().Be(paginatedUsers.Data.Count);
        sut.Data.Should().BeOfType<List<UserView>>();
    }

    private FindAllUsersQueryHandler GenerateQueryHandler =>
        new(_userRepositoryMock.Object);
}