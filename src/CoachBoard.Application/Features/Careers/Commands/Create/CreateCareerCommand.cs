using MediatR;

namespace CoachBoard.Application.Features.Careers.Commands.Create;

public record CreateCareerCommand(
    long UserId,
    string ManagerName,
    string TeamName) : IRequest<long>;