using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Players.Queries.FindAll;
using CoachBoard.Application.ViewModels.Players;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Players.Queries;

public class FindAllPlayersQueryTests
{
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    [Fact(DisplayName = "Should return a page of PlayerView")]
    public async Task ShouldReturnAPageOfPlayerView()
    {
        // Arrange
        var paginatedPlayers = new PaginationResult<Player>()
        {
            Data = PlayerMock.GenerateMany(3)
        };
        var command = new FindAllPlayersQuery(null);
        var commandHandler = GenerateQueryHandler;
        _playerRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(paginatedPlayers);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        sut.Should().BeOfType<PaginationResult<PlayerView>>();
        sut.Data.Count.Should().Be(paginatedPlayers.Data.Count);
    }

    private FindAllPlayersQueryHandler GenerateQueryHandler =>
        new(_playerRepositoryMock.Object);
}