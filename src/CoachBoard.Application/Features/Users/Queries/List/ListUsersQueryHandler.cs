using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.List;
public class ListUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<ListUsersQuery, Result<List<UserResponse>>>
{
    public async Task<Result<List<UserResponse>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.ListAsync(cancellationToken);

        var userResponseList = users
            .Select(user => new UserResponse(
                user.Id,
                user.Username,
                user.Email,
                user.Careers))
            .ToList();

        return Result.Success(userResponseList);
    }
}
