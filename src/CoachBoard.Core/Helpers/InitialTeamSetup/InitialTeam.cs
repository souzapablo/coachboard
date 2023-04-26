using CoachBoard.Core.Entities;

namespace CoachBoard.Core.Helpers.InitialTeamSetup;

public record InitialTeam(
    string Name,
    string Stadium,
    IEnumerable<Player> InitialPlayers);