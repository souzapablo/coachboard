using CoachBoard.Application.ViewModels.Users;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.FindAll;

public record FindAllUsersQuery(
    string? Nickname,
    int Page = 1) : IRequest<PaginationResult<UserView>>;