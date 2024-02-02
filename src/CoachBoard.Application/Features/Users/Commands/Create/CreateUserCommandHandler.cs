using CoachBoard.Domain.Errors;
using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Create;
public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        var emailRegistered = await userRepository.VerifyIfEmailIsRegisteredAsync(request.Email, cancellationToken);

        if (emailRegistered)
            return Result<Guid>.Failure(DomainErrors.User.RegisteredEmail);

        return Result<Guid>.Success(Guid.NewGuid());
    }
}
