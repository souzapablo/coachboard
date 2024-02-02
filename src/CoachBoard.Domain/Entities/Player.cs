using CoachBoard.Domain.Entities.Base;
using CoachBoard.Domain.Enums;

namespace CoachBoard.Domain.Entities;
public class Player(Guid id, string name, int kitNumber, int overall, DateTime birthday, PlayerPosition position)
    : Entity(id)
{
    public string Name { get; private set; } = name;
    public int KitNumber { get; private set; } = kitNumber;
    public int Overall { get; private set; } = overall;
    public DateTime Birthday { get; private set; } = birthday;
    public PlayerPosition Position { get; private set; } = position;
    public Guid TeamId { get; private set; }
    public Team Team { get; private set; } = null!;
}