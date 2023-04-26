using System;
using System.Threading;
using System.Threading.Tasks;
using CoachBoard.Application.Features.Careers.Commands.Create;
using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Infrastructure.Persistence;
using CoachBoard.UnitTests.Mocks;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoachBoard.UnitTests.Features.Careers.Commands;

public class CreateUserCommandTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ICareerRepository> _careerRepositoryMock = new();
    private readonly Mock<IUserRepository> _userRepositoryMock = new();
    private readonly Mock<ITeamRepository> _temRepositoryMock = new();
    private readonly Mock<IPlayerRepository> _playerRepositoryMock = new();

    [Fact(DisplayName = "Should throw exception when user is not found")]
    public async Task ShouldThrowExceptionWhenUserIsNotFound()
    {
        // Arrange
        var command = new CreateCareerCommand(1L, "Test Manager", "Brighton");
        var commandHandler = GenerateCommandHandler;
        _unitOfWorkMock.SetupGet(x => x.Users)
            .Returns(_userRepositoryMock.Object);

        // Act
        Func<Task> sut = async () => await commandHandler.Handle(command, new CancellationToken());

        // Assert
        await sut.Should()
            .ThrowAsync<EntityNotFoundException<User>>()
            .WithMessage("User with id [1] not found");
    }

    [Fact(DisplayName = "Valid command should create a new career")]
    public async Task ValidCommandShouldCreateANewCareer()
    {
        // Arrange
        var user = UserMock.Generate;
        var command = new CreateCareerCommand(user.Id, "Test Manager", "Brighton");
        var commandHandler = GenerateCommandHandler;
        _unitOfWorkMock.SetupGet(x => x.Users)
            .Returns(_userRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Careers)
            .Returns(_careerRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Players)
            .Returns(_playerRepositoryMock.Object);
        _unitOfWorkMock.SetupGet(x => x.Teams)
            .Returns(_temRepositoryMock.Object);
        _userRepositoryMock.Setup(x => x.FindByIdAsync(user.Id))
            .ReturnsAsync(user);

        // Act
        var sut = await commandHandler.Handle(command, new CancellationToken());

        // Assert
        _userRepositoryMock.Verify(x =>
            x.FindByIdAsync(It.IsAny<long>()), Times.Once());
        _careerRepositoryMock.Verify(x =>
            x.CreateAsync(It.IsAny<Career>()), Times.Once);
        sut.Should()
            .BeOfType(typeof(long));
    }

    private CreateCareerCommandHandler GenerateCommandHandler =>
        new(_unitOfWorkMock.Object);
}