using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Delete;
public record DeleteUserCommand(Guid Id) : IRequest<Result>;