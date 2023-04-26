using CoachBoard.Application.Features.Careers.Commands.Create;
using FluentValidation;

namespace CoachBoard.Application.Validators.Careers;

public class CreateCareerCommandValidator : AbstractValidator<CreateCareerCommand>
{
    public CreateCareerCommandValidator()
    {
        RuleFor(command => command.UserId)
            .GreaterThan(0)
            .WithMessage("Invalid user id");

        RuleFor(command => command.ManagerName)
            .NotEmpty()
            .WithMessage("Manger name should be informed");
    }
}