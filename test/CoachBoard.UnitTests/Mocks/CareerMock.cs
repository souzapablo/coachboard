using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;

namespace CoachBoard.UnitTests.Mocks;

public static class CareerMock
{
    public static Career Generate =>
        new Faker<Career>("pt_BR")
            .RuleFor(career => career.Id, faker => faker.IndexFaker + 1)
            .RuleFor(career => career.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(career => career.ManagerName, faker => faker.Person.FullName)
            .Generate();
    
    public static List<Career> GenerateMany(int quantity) =>
        new Faker<Career>("pt_BR")
            .RuleFor(career => career.Id, faker => faker.IndexFaker + 1)
            .RuleFor(career => career.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(career => career.ManagerName, faker => faker.Person.FullName)
            .Generate(quantity);
}