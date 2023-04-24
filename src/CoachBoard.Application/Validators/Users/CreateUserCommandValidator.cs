using CoachBoard.Application.Features.Users.Commands.Create;
using FluentValidation;

namespace CoachBoard.Application.Validators.Users;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Email)
            .EmailAddress()
            .WithMessage("Enter a valid e-mail address");

        RuleFor(command => command.Password)
            .MinimumLength(5)
            .WithMessage("Password must have at least 5 characters");

        RuleFor(command => command.Nickname)
            .MinimumLength(5)
            .WithMessage("Nickname must have at least 3 characters");
    }
}