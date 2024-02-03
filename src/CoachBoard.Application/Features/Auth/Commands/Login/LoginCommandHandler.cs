using CoachBoard.Domain.Helpers;
using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Auth.Commands.Login;
using BCrypt.Net;
using CoachBoard.Domain.Errors;

public class LoginCommandHandler(
    IUserRepository _userRepository, 
    IJwtProvider _jwtProvider) : IRequestHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsername(request.Username, cancellationToken);

        if (user is null)
            return Result.Failure<string>(DomainErrors.Auth.InvalidCredentials);

        var validPassword = BCrypt.Verify(request.Password, user.PasswordHash);

        if (!validPassword)
            return Result.Failure<string>(DomainErrors.Auth.InvalidCredentials);

        var token = _jwtProvider.GenerateToken(user);

        return Result.Success(token);
    }
}
