using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Opponents.Queries.FindAll;
using CoachBoard.Application.ViewModels.Opponents;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Opponents.Queries;

public class FindAllOpponentsQueryTests
{
    private readonly Mock<IOpponentRepository> _opponentRepositoryMcMock = new();

    [Fact(DisplayName = "Should return a page of OpponentView")]
    public async Task  ShouldReturnAPageOfOpponentView()
    {
        // Arrange
        var paginatedOpponents = new PaginationResult<Opponent>()
        {
            Data = OpponentMock.GenerateMany(3)
        };
        var command = new FindAllOpponentsQuery(null);
        var commandHandler = GenerateQueryHandler;
        _opponentRepositoryMcMock.Setup(x => x.FindAllAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(paginatedOpponents);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        sut.Should().BeOfType<PaginationResult<OpponentView>>();
        sut.Data.Count.Should().Be(paginatedOpponents.Data.Count);
    }

    private FindAllOpponentsQueryHandler GenerateQueryHandler =>
        new(_opponentRepositoryMcMock.Object);
}