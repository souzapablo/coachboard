using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Careers.Queries.FindAll;
using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Careers.Queries;

public class FindAllCareersQueryTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock = new();

    [Fact(DisplayName = "Should return a page of CareerView")]
    public async Task ShouldReturnAPageOfCareerView()
    {
        // Arrange
        var paginatedCareers = new PaginationResult<Career>
        {
            Data = CareerMock.GenerateMany(3)
        };
        var query = new FindAllCareersQuery(null);
        var queryHandler = GenerateQueryHandler;
        _careerRepositoryMock.Setup(x => x.FindAllAsync(It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(paginatedCareers);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Data.Count.Should().Be(paginatedCareers.Data.Count);
        sut.Data.Should().BeOfType<List<CareerView>>();
    }

    private FindAllCareersQueryHandler GenerateQueryHandler =>
        new(_careerRepositoryMock.Object);
}