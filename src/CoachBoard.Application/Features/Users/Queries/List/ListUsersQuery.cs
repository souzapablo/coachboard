using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.List;
public record ListUsersQuery : IRequest<Result<List<UserResponse>>>;
