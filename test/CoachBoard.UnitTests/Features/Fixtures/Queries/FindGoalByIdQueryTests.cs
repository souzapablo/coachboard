using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Goals.Queries.FindById;
using CoachBoard.Application.ViewModels.Goals;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Fixtures.Queries;

public class FindGoalByIdQueryTests
{
    private readonly Mock<IGoalRepository> _goalRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception if Goal is not found")]
    public async Task ShouldThrowExceptionIfGoalIsNotFound()
    {
        // Arrange
        var query = new FindGoalByIdQuery(1L);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Goal>>()
            .WithMessage("Goal with id [1] not found");
    }

    [Fact(DisplayName = "Should return GoalDetails if Goal is found")]
    public async Task ShouldReturnGoalDetailsIfGoalIsFound()
    {
        // Arrange
        var goal = GoalMock.Generate;
        var query = new FindGoalByIdQuery(goal.Id);
        var queryHandler = GenerateQueryHandler;
        _goalRepositoryMock.Setup(x => x.FindByIdAsync(goal.Id,
                t => t.PlayerScored,
                t => t.Assist!.PlayerAssisted))
            .ReturnsAsync(goal);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<GoalDetailsView>();
        sut.PlayerScored.Should().Be(goal.PlayerScored?.Name);
        sut.PlayerAssisted.Should().Be(goal.Assist?.PlayerAssisted.Name);
    }

    private FindGoalByIdQueryHandler GenerateQueryHandler =>
        new(_goalRepositoryMock.Object);
}