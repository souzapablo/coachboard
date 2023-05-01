using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;
using FluentAssertions;

namespace CoachBoard.UnitTests.Mocks;

public static class GoalMock
{
    public static Goal Generate =>
        new Faker<Goal>("pt_BR")
            .RuleFor(goal => goal.Id, faker => faker.IndexFaker + 1)
            .RuleFor(goal => goal.FixtureId, faker => faker.IndexFaker + 1)
            .RuleFor(goal => goal.PlayerScored, PlayerMock.Generate)
            .RuleFor(goal => goal.Assist, AssistMock.Generate)
            .Generate();
    
    public static List<Goal> GenerateMany(int quantity) =>
        new Faker<Goal>("pt_BR")
            .RuleFor(goal => goal.Id, faker => faker.IndexFaker + 1)
            .RuleFor(goal => goal.PlayerScored, PlayerMock.Generate)
            .RuleFor(goal => goal.Assist, AssistMock.Generate)
            .Generate(quantity);

}