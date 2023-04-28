using CoachBoard.Core.Enums;

namespace CoachBoard.Application.InputModels.Players;

public record CreatePlayerInput(
    string Name,
    DateTime BirthDate,
    DateTime? JoinedDate,
    int Overall,
    int KitNumber,
    PlayerPosition Position,
    PlayerStatus Status);