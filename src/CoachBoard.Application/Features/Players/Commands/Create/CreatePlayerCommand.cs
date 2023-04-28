using CoachBoard.Core.Enums;
using MediatR;

namespace CoachBoard.Application.Features.Players.Commands.Create;

public record CreatePlayerCommand(
    long TeamId, 
    string Name, 
    DateTime? JoinedDate, 
    DateTime BirthDate, 
    int Overall, 
    PlayerPosition Position, 
    int KitNumber, 
    PlayerStatus Status) : IRequest<long>;