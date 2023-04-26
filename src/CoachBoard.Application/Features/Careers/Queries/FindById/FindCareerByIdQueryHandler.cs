using CoachBoard.Application.ViewModels.Careers;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Queries.FindById;

public class FindCareerByIdQueryHandler : IRequestHandler<FindCareerByIdQuery, CareerDetailsView>
{
    private readonly ICareerRepository _careerRepository;

    public FindCareerByIdQueryHandler(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    public async Task<CareerDetailsView> Handle(FindCareerByIdQuery request, CancellationToken cancellationToken)
    {
        var career = await _careerRepository.FindByIdAsync(request.Id);

        if (career is null)
            throw new EntityNotFoundException<Career>(request.Id);

        return CareerDetailsView.Map(career);
    }
}