using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Fixtures.Queries.FindById;
using CoachBoard.Application.ViewModels.Fixtures;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Fixtures.Queries;

public class FindFixtureByIdQueryTests
{
    private readonly Mock<IFixtureRepository> _fixtureRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception if Fixture is not found")]
    public async Task ShouldThrowExceptionIfFixtureIsNotFound()
    {
        // Arrange
        var query = new FindFixtureByIdQuery(1);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Fixture>>()
            .WithMessage("Fixture with id [1] not found");
    }

    [Fact(DisplayName = "Should return FixtureDetails if opponent is found")]
    public async Task ShouldReturnTeamDetailsViewIfUserIsFound()
    {
        // Arrange
        var fixtureMock = FixtureMock.Generate;
        var query = new FindFixtureByIdQuery(fixtureMock.Id);
        var queryHandler = GenerateQueryHandler;
        _fixtureRepositoryMock.Setup(x => x.FindByIdAsync(fixtureMock.Id,
                fixture => fixture.Goals,
                fixture => fixture.LineUp,
                fixture => fixture.Assists))
            .ReturnsAsync(fixtureMock);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<FixtureDetailsView>();
        sut.Id.Should().Be(fixtureMock.Id);
        sut.TeamId.Should().Be(fixtureMock.TeamId);
        sut.OpponentId.Should().Be(fixtureMock.OpponentId);
        sut.Goals.Count().Should().Be(fixtureMock.Goals.Count);
        sut.Competition.Should().Be(fixtureMock.Competition);
        sut.Location.Should().Be(fixtureMock.Location);
        sut.LineUp.Count().Should().Be(fixtureMock.LineUp.Count);
    }

    private FindFixtureByIdQueryHandler GenerateQueryHandler =>
        new(_fixtureRepositoryMock.Object);
}