using CoachBoard.Domain.Errors;
using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.GetById;
public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            return Result.Failure<UserResponse>(DomainErrors.User.UserNotFound(request.Id));

        var response = new UserResponse(user.Id, user.Username, user.Email, user.Careers);

        return Result.Success(response);
    }
}
