using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Careers.Queries.FindById;
using CoachBoard.Application.Repositories;
using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Careers.Queries;

public class FindCareerByIdQueryTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when career is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var query = new FindCareerByIdQuery(1L);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<Career>>()
            .WithMessage("Career with id [1] not found");
    }

    [Fact(DisplayName = "Should return CareerDetailsView if career is found")]
    public async Task ShouldReturnUserDetailsViewIfUserIsFound()
    {
        // Arrange
        var queryHandler = GenerateQueryHandler;
        var career = CareerMock.Generate;
        var query = new FindCareerByIdQuery(career.Id);
        _careerRepositoryMock.Setup(x => x.FindByIdAsync(career.Id))
            .ReturnsAsync(career);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Should().BeOfType<CareerDetailsView>();
        sut.Id.Should().Be(career.Id);
        sut.Manager.Should().Be(career.ManagerName);
    }

    private FindCareerByIdQueryHandler GenerateQueryHandler =>
        new(_careerRepositoryMock.Object);
}