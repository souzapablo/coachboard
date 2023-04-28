using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Players.Queries.FindById;
using CoachBoard.Application.ViewModels.Players;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Players.Queries;

public class FindPlayerByIdQueryTests
{
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception if Player is not found")]
    public async Task ShouldThrowExceptionIfOpponentIsNotFound()
    {
        // Arrange
        var query = new FindPlayerByIdQuery(1);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Player>>()
            .WithMessage("Player with id [1] not found");
    }

    [Fact(DisplayName = "Should return PlayerDetailsView if opponent is found")]
    public async Task ShouldReturnPlayerDetailsViewIfPlayerIsFound()
    {
        // Arrange
        var player = PlayerMock.Generate;
        var query = new FindPlayerByIdQuery(player.Id);
        var queryHandler = GenerateQueryHandler;
        _playerRepositoryMock.Setup(x => x.FindByIdAsync(player.Id))
            .ReturnsAsync(player);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<PlayerDetailsView>();
        sut.Id.Should().Be(player.Id);
        sut.TeamId.Should().Be(player.TeamId);
        sut.Name.Should().Be(player.Name);
        sut.Age.Should().Be(player.Age);
        sut.PlayerFixtures.Should().Be(player.Fixtures.Count);
        sut.PlayerAssists.Should().Be(player.Assists.Count);
        sut.PlayerGoals.Should().Be(player.Goals.Count);
    }

    private FindPlayerByIdQueryHandler GenerateQueryHandler =>
        new(_playerRepositoryMock.Object);
}