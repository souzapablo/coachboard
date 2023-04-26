using CoachBoard.Application.Features.Users.Commands.ChangeRole;
using FluentValidation;

namespace CoachBoard.Application.Validators.Users;

public class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
{
    public ChangeRoleCommandValidator()
    {
        RuleFor(command => command.Id)
            .GreaterThan(0)
            .WithMessage("Invalid user id");
        
        RuleFor(command => command.NewRole)
            .IsInEnum()
            .WithMessage("Invalid role");
    }
}