using CoachBoard.Application.Repositories;
using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Queries.FindByUserId;

public class FindCareersByUserIdQueryHandler : IRequestHandler<FindCareersByUserIdQuery, PaginationResult<CareerView>>
{
    private readonly ICareerRepository _careerRepository;
    private readonly IUserRepository _userRepository;

    public FindCareersByUserIdQueryHandler(ICareerRepository careerRepository, IUserRepository userRepository)
    {
        _careerRepository = careerRepository;
        _userRepository = userRepository;
    }

    public async Task<PaginationResult<CareerView>> Handle(FindCareersByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.UserId);

        if (user is null)
            throw new EntityNotFoundException<User>(request.UserId);

        var careers = await _careerRepository.FindByUserIdAsync(user.Id, request.Page);

        var careersView = new PaginationResult<CareerView>
        {
            ItemsCount = careers.ItemsCount,
            Page = careers.Page,
            PageSize = careers.PageSize,
            TotalPages = careers.TotalPages,
            Data = careers.Data.Select(CareerView.Map).ToList()
        };
        return careersView;
    }
}