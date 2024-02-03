using CoachBoard.Domain.Errors;
using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Commands.Delete;
public class DeleteUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, Result>
{
    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            return Result.Failure(DomainErrors.User.UserNotFound(request.Id));

        user.Delete();

        userRepository.Update(user);

        await unitOfWork.SaveChangesAsync();    

        return Result.Success();    
    }
}
