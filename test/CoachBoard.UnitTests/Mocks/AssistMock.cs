using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;

namespace CoachBoard.UnitTests.Mocks;

public static class AssistMock
{
    public static Assist Generate =>
        new Faker<Assist>("pt_BR")
            .RuleFor(assist => assist.Id, faker => faker.IndexFaker + 1)
            .RuleFor(assist => assist.GoalId, faker => faker.IndexFaker + 1)
            .RuleFor(assist => assist.PlayerAssisted, PlayerMock.Generate)
            .Generate();
    
    public static List<Assist> GenerateMany(int quantity) =>
        new Faker<Assist>("pt_BR")
            .RuleFor(assist => assist.Id, faker => faker.IndexFaker + 1)
            .RuleFor(assist => assist.GoalId, faker => faker.IndexFaker + 1)
            .RuleFor(assist => assist.PlayerAssisted, PlayerMock.Generate)
            .Generate(quantity);
}