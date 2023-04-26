using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Commands.Delete;

public class DeleteCareerCommandHandler : IRequestHandler<DeleteCareerCommand, Unit>
{
    private readonly ICareerRepository _careerRepository;

    public DeleteCareerCommandHandler(ICareerRepository careerRepository)
    {
        _careerRepository = careerRepository;
    }

    public async Task<Unit> Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
    {
        var career = await _careerRepository.FindByIdAsync(request.Id);

        if (career is null)
            throw new EntityNotFoundException<Career>(request.Id);
        
        career.Delete();

        await _careerRepository.UpdateAsync(career);

        return Unit.Value;
    }
}