using FluentValidation;

namespace CoachBoard.Application.Features.Users.Commands.Create;
internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(input => input.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid e-mail.");

        RuleFor(input => input.Password)
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
            .WithMessage("Password must be at least 8 characters long and contain at least one lowercase letter, one uppercase letter, and one digit.");

        RuleFor(input => input.Username)
            .NotEmpty()
            .Length(5, 18)
            .WithMessage("Username must have between 5 and 18 characters.");
    }
}
