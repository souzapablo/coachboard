using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Player : BaseEntity
{
    public Player(string name, DateTime birthDate, DateTime joinedDate, int overall, int kitNumber, PlayerPosition position)
    {
        Name = name;
        BirthDate = birthDate;
        JoinedDate = joinedDate;
        Overall = overall;
        KitNumber = kitNumber;
        Position = position;
    }

    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime JoinedDate { get; private set; }
    public int Overall { get; private set; }
    public int KitNumber { get; private set; }
    public PlayerPosition Position { get; private set; }
}