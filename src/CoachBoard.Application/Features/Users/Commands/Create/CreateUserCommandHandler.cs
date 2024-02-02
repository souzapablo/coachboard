using CoachBoard.Domain.Entities;
using CoachBoard.Domain.Errors;
using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Create;
using BCrypt.Net;

public class CreateUserCommandHandler(IUserRepository userRepository , IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
    {
        var emailRegistered = await userRepository.VerifyIfEmailIsRegisteredAsync(request.Email, cancellationToken);

        if (emailRegistered)
            return Result<Guid>.Failure(DomainErrors.User.RegisteredEmail);

        var passwordHash = BCrypt.HashPassword(request.Password);

        var user = new User(Guid.NewGuid(), request.Username, request.Email, passwordHash);

        userRepository.Create(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(user.Id);
    }
}
