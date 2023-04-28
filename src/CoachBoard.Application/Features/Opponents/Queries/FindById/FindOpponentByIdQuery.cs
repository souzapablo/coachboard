using CoachBoard.Application.ViewModels.Opponents;
using MediatR;

namespace CoachBoard.Application.Features.Opponents.Queries.FindById;

public record FindOpponentByIdQuery(
    long Id) : IRequest<OpponentDetailsView>;