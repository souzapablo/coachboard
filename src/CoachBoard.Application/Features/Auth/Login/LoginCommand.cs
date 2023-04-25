using CoachBoard.Application.ViewModels.Auth;
using MediatR;

namespace CoachBoard.Application.Features.Auth.Login;

public record LoginCommand(
    string Nickname,
    string Password) : IRequest<LoginView>;