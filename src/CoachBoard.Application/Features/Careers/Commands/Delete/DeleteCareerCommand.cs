using MediatR;

namespace CoachBoard.Application.Features.Careers.Commands.Delete;

public record DeleteCareerCommand(
    long Id) : IRequest<Unit>;