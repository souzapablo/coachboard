using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using CoachBoard.Core.Repositories;
using MediatR;

namespace CoachBoard.Application.Features.Opponents.Commands.Create;

public class CreateOpponentCommandHandler : IRequestHandler<CreateOpponentCommand, long>
{
    private readonly IOpponentRepository _opponentRepository;
    private readonly ICareerRepository _careerRepository;

    public CreateOpponentCommandHandler(IOpponentRepository opponentRepository, ICareerRepository careerRepository)
    {
        _opponentRepository = opponentRepository;
        _careerRepository = careerRepository;
    }

    public async Task<long> Handle(CreateOpponentCommand request, CancellationToken cancellationToken)
    {
        var career = await _careerRepository.FindByIdAsync(request.CareerId);

        if (career is null)
            throw new EntityNotFoundException<Career>(request.CareerId);

        var opponent = new Opponent(career.Id,
            request.Name,
            request.Stadium);

        await _opponentRepository.CreateAsync(opponent);

        await _opponentRepository.SaveChangesAsync();

        return opponent.Id;
    }
}