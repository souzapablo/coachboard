using System.Collections.Generic;
using Bogus;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Enums;

namespace CoachBoard.UnitTests.Mocks;

public static class UserMock
{
    public static User Generate =>
        new Faker<User>("pt_BR")
            .RuleFor(user => user.Id, faker => faker.IndexFaker + 1)
            .RuleFor(user => user.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(user => user.Nickname, faker => faker.Person.UserName)
            .RuleFor(user => user.Email, faker => faker.Person.Email)
            .RuleFor(user => user.Password, faker => faker.Lorem.Word())
            .RuleFor(user => user.Role, Role.Standard)
            .Generate();
    
    public static List<User> GenerateMany(int quantity) =>
        new Faker<User>("pt_BR")
            .RuleFor(user => user.Id, faker => faker.IndexFaker + 1)
            .RuleFor(user => user.CreatedAt, faker => faker.Date.Past(3))
            .RuleFor(user => user.Nickname, faker => faker.Person.UserName)
            .RuleFor(user => user.Email, faker => faker.Person.Email)
            .RuleFor(user => user.Password, faker => faker.Lorem.Word())
            .RuleFor(user => user.Role, Role.Standard)
            .Generate(quantity);
}