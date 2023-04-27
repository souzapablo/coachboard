using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Teams.Queries.FindById;
using CoachBoard.Application.ViewModels.Teams;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Teams.Queries;

public class FindTeamByIdQueryTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when team is not found")]
    public async Task ShouldThrowExceptionWhenTeamIsNotFound()
    {
        // Arrange
        var query = new FindTeamByIdQuery(1L);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Team>>()
            .WithMessage("Team with id [1] not found");
    }

    [Fact(DisplayName = "Should return TeamDetailsView if user is found")]
    public async Task ShouldReturnTeamDetailsViewIfUserIsFound()
    {
        // Arrange
        var team = TeamMock.Generate;
        var query = new FindTeamByIdQuery(team.Id);
        var queryHandler = GenerateQueryHandler;
        _teamRepositoryMock.Setup(x => x.FindByIdAsync(team.Id))
            .ReturnsAsync(team);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<TeamDetailsView>();
        sut.Id.Should().Be(team.Id);
        sut.CareerId.Should().Be(team.CareerId);
        sut.Name.Should().Be(team.Name);
        sut.Squad.Count.Should().Be(team.Squad.Count);
    }

    private FindTeamByIdQueryHandler GenerateQueryHandler =>
        new(_teamRepositoryMock.Object);
}