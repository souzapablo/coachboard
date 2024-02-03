using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.List;
public record ListUsersQuery(
    int Page,
    int PageSize) : IRequest<PaginatedResult<UserResponse>>;
