using CoachBoard.Application.Repositories;
using CoachBoard.Core.Entities;
using CoachBoard.Core.Exceptions;
using MediatR;

namespace CoachBoard.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler : IRequestHandler<CreateCareerCommand, long>
{
    private readonly ICareerRepository _careerRepository;
    private readonly IUserRepository _userRepository;

    public CreateCareerCommandHandler(ICareerRepository careerRepository, IUserRepository userRepository)
    {
        _careerRepository = careerRepository;
        _userRepository = userRepository;
    }

    public async Task<long> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.UserId);

        if (user is null)
            throw new EntityNotFoundException<User>(request.UserId);

        var career = new Career(user.Id, request.ManagerName);

        await _careerRepository.CreateAsync(career);

        return career.Id;
    }
}