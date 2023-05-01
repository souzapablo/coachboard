using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.UnitTests.Mocks;

public static class FixtureMock
{
    public static Fixture Generate =>
        new Faker<Fixture>("pt_BR")
            .RuleFor(fixture => fixture.Id, faker => faker.IndexFaker + 1)
            .RuleFor(fixture => fixture.TeamId, faker => faker.IndexFaker + 1)
            .RuleFor(fixture => fixture.OpponentId, faker => faker.IndexFaker + 1)
            .RuleFor(fixture => fixture.Location, faker => faker.Random.Enum<FixtureLocation>())
            .RuleFor(fixture => fixture.Competition, faker => faker.Random.Enum<Competition>())
            .RuleFor(fixture => fixture.LineUp, faker => PlayerMock.GenerateMany(5))
            .RuleFor(fixture => fixture.Goals, faker => GoalMock.GenerateMany(faker.Random.Int(0, 8)))
            .Generate();
    
    public static List<Fixture> GenerateMany(int quantity) =>
        new Faker<Fixture>("pt_BR")
            .RuleFor(fixture => fixture.Id, faker => faker.IndexFaker + 1)
            .RuleFor(fixture => fixture.TeamId, faker => faker.IndexFaker + 1)
            .RuleFor(fixture => fixture.OpponentId, faker => faker.IndexFaker + 1)
            .RuleFor(fixture => fixture.Location, faker => faker.Random.Enum<FixtureLocation>())
            .RuleFor(fixture => fixture.Competition, faker => faker.Random.Enum<Competition>())
            .Generate(quantity);
}