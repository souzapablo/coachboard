using CoachBoard.Application.Features.Opponents.Commands.Create;
using FluentValidation;

namespace CoachBoard.Application.Validators.Opponents;

public class CreateOpponentCommandValidator : AbstractValidator<CreateOpponentCommand>
{
    public CreateOpponentCommandValidator()
    {
        RuleFor(command => command.CareerId)
            .GreaterThan(0)
            .WithMessage("Invalid career id");

        RuleFor(command => command.Name)
            .MinimumLength(3)
            .WithMessage("Opponent name should have at least 3 characters");

        RuleFor(command => command.Stadium)
            .MinimumLength(3)
            .WithMessage("Opponent stadium should have at least 3 characters");
    }
}