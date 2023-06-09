﻿using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;

namespace CoachBoard.UnitTests.Mocks;

public static class OpponentMock
{
    public static Opponent Generate =>
        new Faker<Opponent>("pt_BR")
            .RuleFor(opponent => opponent.Id, faker => faker.IndexFaker + 1)
            .RuleFor(opponent => opponent.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(opponent => opponent.Name, faker => faker.Person.FirstName)
            .RuleFor(opponent => opponent.Stadium, faker => faker.Person.FullName)
            .RuleFor(opponent => opponent.CareerId, faker => faker.IndexFaker + 1)
            .Generate();

    public static List<Opponent> GenerateMany(int quantity) =>
        new Faker<Opponent>("pt_BR")
            .RuleFor(opponent => opponent.Id, faker => faker.IndexFaker + 1)
            .RuleFor(opponent => opponent.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(opponent => opponent.Name, faker => faker.Person.FirstName)
            .RuleFor(opponent => opponent.Stadium, faker => faker.Person.FullName)
            .RuleFor(opponent => opponent.CareerId, faker => faker.IndexFaker + 1)
            .Generate(quantity);
}