using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Services;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public CreateUserCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.FindByEmailAsync(request.Email))
            throw new EmailAlreadyRegisteredException();
        
        var hashPassword = _authService
            .ComputeSha256Hash(request.Password);

        var newUser = new User(
            request.Nickname,
            request.Email,
            hashPassword);

        await _userRepository.CreateAsync(newUser);

        return newUser.Id;
    }
}