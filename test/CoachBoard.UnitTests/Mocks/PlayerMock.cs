using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;

namespace CoachBoard.UnitTests.Mocks;

public static class PlayerMock
{
    public static Player Generate =>
        new Faker<Player>("pt_BR")
            .RuleFor(player => player.Id, faker => faker.IndexFaker + 1)
            .RuleFor(player => player.CreatedAt, faker => faker.Date.Past(3))
            .Generate();

    public static List<Player> GenerateMany(int quantity) =>
        new Faker<Player>("pt_BR")
            .RuleFor(player => player.Id, faker => faker.IndexFaker + 1)
            .RuleFor(player => player.CreatedAt, faker => faker.Date.Past(3))
            .Generate(quantity);
}