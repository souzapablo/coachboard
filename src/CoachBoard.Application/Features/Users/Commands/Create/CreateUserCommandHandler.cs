using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Create;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
