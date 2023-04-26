using CoachBoard.Application.ViewModels.Users;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Users.Queries.FindById;

public class FindUserByIdQueryHandler : IRequestHandler<FindUserByIdQuery, UserDetailsView>
{
    private readonly IUserRepository _userRepository;

    public FindUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDetailsView> Handle(FindUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.Id);

        if (user is null)
            throw new EntityNotFoundException<User>(request.Id);

        return UserDetailsView.Map(user);
    }
}