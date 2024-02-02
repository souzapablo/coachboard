using CoachBoard.Domain.Shared;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.GetById;
public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserResponse>>;