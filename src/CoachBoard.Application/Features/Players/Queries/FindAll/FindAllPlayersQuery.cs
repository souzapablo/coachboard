using CoachBoard.Application.ViewModels.Players;
using CoachBoard.Core.Models;
using MediatR;

namespace CoachBoard.Application.Features.Players.Queries.FindAll;

public record FindAllPlayersQuery(
    string? Name,
    int Page = 1) : IRequest<PaginationResult<PlayerView>>;