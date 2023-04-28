using CoachBoard.Application.Features.Players.Commands.Create;
using FluentValidation;

namespace CoachBoard.Application.Validators.Players;

public class CratePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CratePlayerCommandValidator()
    {
        RuleFor(player => player.KitNumber)
            .InclusiveBetween(1, 99)
            .WithMessage("Player kit number must be between 1 a 99");
        
        RuleFor(player => player.Overall)
            .InclusiveBetween(1, 99)
            .WithMessage("Player overall must be between 1 a 99");

        RuleFor(player => player.BirthDate.Year)
            .LessThanOrEqualTo(2004)
            .WithMessage("Player must have at least 15 years");

        RuleFor(player => player.Name)
            .MinimumLength(3)
            .WithMessage("Player name must have at least 5 characters");

        RuleFor(player => player.Position)
            .IsInEnum()
            .WithMessage("Invalid player position");

        RuleFor(player => player.Status)
            .IsInEnum()
            .WithMessage("Invalid player status");
        
        RuleFor(player => player.TeamId)
            .GreaterThan(0)
            .WithMessage("Invalid team id");
    }
}