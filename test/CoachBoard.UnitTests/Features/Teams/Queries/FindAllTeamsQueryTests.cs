using CoachBoard.Application.Features.Teams.Queries.FindAll;
using CoachBoard.Application.ViewModels.Teams;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.UnitTests.Mocks;
using Xunit;

namespace CoachBoard.UnitTests.Features.Teams.Queries;

public class FindAllTeamsQueryTests
{
    private readonly Mock<ITeamRepository> _teamRepositoryMock = new();

    [Fact(DisplayName = "Should return a page of TeamView")]
    public async Task ShouldReturnAPageOfCareerView()
    {
        // Arrange
        var paginatedTeams = new PaginationResult<Team>
        {
            Data = TeamMock.GenerateMany(3)
        };
        var query = new FindAllTeamsQuery(null);
        var queryHandler = GenerateQueryHandler;
        _teamRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(paginatedTeams);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Data.Count.Should().Be(3);
        sut.Data.Should().BeOfType<List<TeamView>>();
    }

    private FindAllTeamsQueryHandler GenerateQueryHandler =>
        new(_teamRepositoryMock.Object);
}