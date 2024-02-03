using CoachBoard.Application.Extensions;
using CoachBoard.Domain.Repositories;
using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.List;
public class ListUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<ListUsersQuery, PaginatedResult<UserResponse>>
{
    public async Task<PaginatedResult<UserResponse>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        var users = userRepository.List();

        var userResponses = users
            .Select(user => new UserResponse(user.Id, user.Username, user.Email, user.Careers))
            .AsQueryable();

        return await userResponses.CreatePaginationAsync(request.Page, request.PageSize);
    }
}
