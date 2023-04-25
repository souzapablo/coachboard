using CoachBoard.Application.Repositories;
using CoachBoard.Application.ViewModels.Auth;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Extensions;
using CoachBoard.Core.Services;
using MediatR;

namespace CoachBoard.Application.Features.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginView>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<LoginView> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _authService.ComputeSha256Hash(request.Password);

        var user = await _userRepository.FindByNicknameAndPasswordAsync(request.Nickname, passwordHash);

        if (user is null)
            throw new InvalidLoginException();

        var token = _authService.GenerateJwtToken(user.Nickname, EnumExtensions.GetDescription(user.Role));

        return new LoginView(token);
    }
}