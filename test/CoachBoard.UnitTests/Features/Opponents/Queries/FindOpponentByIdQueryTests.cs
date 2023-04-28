using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Opponents.Queries.FindById;
using CoachBoard.Application.ViewModels.Opponents;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Opponents.Queries;

public class FindOpponentByIdQueryTests
{
    private readonly Mock<IOpponentRepository> _opponentRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception if Opponent is not found")]
    public async Task ShouldThrowExceptionIfOpponentIsNotFound()
    {
        // Arrange
        var query = new FindOpponentByIdQuery(1);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Opponent>>()
            .WithMessage("Opponent with id [1] not found");
    }

    [Fact(DisplayName = "Should return OpponentDetails if opponent is found")]
    public async Task ShouldReturnTeamDetailsViewIfUserIsFound()
    {
        // Arrange
        var opponent = OpponentMock.Generate;
        var query = new FindOpponentByIdQuery(opponent.Id);
        var queryHandler = GenerateQueryHandler;
        _opponentRepositoryMock.Setup(x => x.FindByIdAsync(opponent.Id))
            .ReturnsAsync(opponent);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<OpponentDetailsView>();
        sut.Id.Should().Be(opponent.Id);
        sut.CareerId.Should().Be(opponent.CareerId);
        sut.Name.Should().Be(opponent.Name);
        sut.Stadium.Should().Be(opponent.Stadium);
        sut.PlayedAgainst.Should().Be(opponent.Fixtures.Count);
    }

    private FindOpponentByIdQueryHandler GenerateQueryHandler =>
        new(_opponentRepositoryMock.Object);
}