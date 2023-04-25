using CoachBoard.Core.Enums;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.ChangeRole;

public record ChangeRoleCommand(
    long Id,
    Role NewRole) : IRequest<Unit>;