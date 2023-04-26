using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Models;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Queries.FindAll;

public class FindAllCareersQueryHandler : IRequestHandler<FindAllCareersQuery, PaginationResult<CareerView>>
{
    private readonly ICareerRepository _careerRepository;

    public FindAllCareersQueryHandler(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    public async Task<PaginationResult<CareerView>> Handle(FindAllCareersQuery request, CancellationToken cancellationToken)
    {
        var careers = await _careerRepository.FindAllAsync(request.CareerManager, request.Page);
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