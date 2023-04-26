using CoachBoard.Application.ViewModels.Users;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.FindAll;

public class FindAllUsersQueryHandler : IRequestHandler<FindAllUsersQuery, PaginationResult<UserView>>
{
    private readonly IUserRepository _userRepository;

    public FindAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PaginationResult<UserView>> Handle(FindAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.FindAllAsync(request.Nickname, request.Page);
        var usersView = new PaginationResult<UserView>
        {
            ItemsCount = users.ItemsCount,
            Page = users.Page,
            PageSize = users.PageSize,
            TotalPages = users.TotalPages,
            Data = users.Data.Select(UserView.Map).ToList()
        };
        return usersView;
    }
}