using CoachBoard.Application.ViewModels.Players;
using MediatR;

namespace CoachBoard.Application.Features.Players.Queries.FindById;

public record FindPlayerByIdQuery(
    long Id) : IRequest<PlayerDetailsView>;