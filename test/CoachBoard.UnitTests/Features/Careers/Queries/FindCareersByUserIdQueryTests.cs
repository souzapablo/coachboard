using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Careers.Queries.FindByUserId;
using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Careers.Queries;

public class FindCareersByUserIdQueryTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when career is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var query = new FindCareersByUserIdQuery(1L, 1);
        var queryHandler = GenerateQueryHandler;

        // Act
        Func<Task> sut = async () => await queryHandler.Handle(query, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<User>>()
            .WithMessage("User with id [1] not found");
    }

    [Fact(DisplayName = "Should return a page of CareerView")]
    public async Task ShouldReturnAPageOfCareerView()
    {
        // Arrange
        var user = UserMock.Generate;
        var paginatedCareers = new PaginationResult<Career>
        {
            Data = new List<Career>
            {
                new(1L, "Jorge Sampaoli"),
                new(2L, "Jorge Jesus"),
                new(1L, "Vanderlei Luxemburgo")
            }
        };
        var query = new FindCareersByUserIdQuery(1L, 1);
        var queryHandler = GenerateQueryHandler;
        _userRepositoryMock.Setup(x => x.FindByIdAsync(1L))
            .ReturnsAsync(user);
        _careerRepositoryMock.Setup(x => x.FindByUserIdAsync(user.Id, It.IsAny<int>()))
            .ReturnsAsync(paginatedCareers);

        // Act
        var sut = await queryHandler.Handle(query, new CancellationToken());

        // Assert
        sut.Data.Count.Should().Be(3);
        sut.Data.Should().BeOfType<List<CareerView>>();
    }

    private FindCareersByUserIdQueryHandler GenerateQueryHandler =>
        new(_careerRepositoryMock.Object,
            _userRepositoryMock.Object);
}