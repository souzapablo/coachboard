using CoachBoard.Application.ViewModels.Users;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.FindById;

public record FindUserByIdQuery(
    long Id) : IRequest<UserDetailsView>;