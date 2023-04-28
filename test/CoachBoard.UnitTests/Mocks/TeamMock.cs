using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;

namespace CoachBoard.UnitTests.Mocks;

public static class TeamMock
{
    public static Team Generate =>
        new Faker<Team>("pt_BR")
            .RuleFor(team => team.Id, faker => faker.IndexFaker + 1)
            .RuleFor(team => team.CareerId, faker => faker.IndexFaker + 1)
            .RuleFor(team => team.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(team => team.Stadium, faker => faker.Person.FullName)
            .RuleFor(team => team.Name, faker => faker.Lorem.Word())
            .RuleFor(team => team.Squad, new List<Player>())
            .Generate();

    public static List<Team> GenerateMany(int quantity) =>
        new Faker<Team>("pt_BR")
            .RuleFor(team => team.Id, faker => faker.IndexFaker + 1)
            .RuleFor(team => team.CareerId, faker => faker.IndexFaker + 1)
            .RuleFor(team => team.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(team => team.Stadium, faker => faker.Person.FullName)
            .RuleFor(team => team.Name, faker => faker.Lorem.Word())
            .RuleFor(team => team.Squad, new List<Player>())
            .Generate(quantity);
}