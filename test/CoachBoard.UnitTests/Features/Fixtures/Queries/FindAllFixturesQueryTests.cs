using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Fixtures.Queries.FindAll;
using CoachBoard.Application.ViewModels.Fixtures;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Fixtures.Queries;

public class FindAllFixturesQueryTests
{
    private readonly Mock<IFixtureRepository> _fixtureRepositoryMock = new();

    [Fact(DisplayName = "Should return a page of FixtureView")]
    public async Task ShouldReturnAPageOfFixtureView()
    {
        // Arrange
        var paginatedFixtures = new PaginationResult<Fixture>()
        {
            Data = FixtureMock.GenerateMany(3)
        };
        var command = new FindAllFixturesQuery();
        var commandHandler = GenerateQueryHandler;
        _fixtureRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<int>()))
            .ReturnsAsync(paginatedFixtures);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        sut.Should().BeOfType<PaginationResult<FixtureView>>();
        sut.Data.Count.Should().Be(paginatedFixtures.Data.Count);
    }

    private FindAllFixturesQueryHandler GenerateQueryHandler =>
        new(_fixtureRepositoryMock.Object);
}