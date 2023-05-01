using MediatR;

namespace CoachBoard.Application.Features.Fixtures.Commands.Delete;

public record DeleteFixtureCommand(
    long Id) : IRequest<Unit>;