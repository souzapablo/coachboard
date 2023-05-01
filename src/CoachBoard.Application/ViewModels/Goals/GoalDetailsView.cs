using CoachBoard.Application.ViewModels.Players;

namespace CoachBoard.Application.ViewModels.Goals;

public record GoalDetailsView(
    long Id,
    string? PlayerScored,
    string? PlayerAssisted);