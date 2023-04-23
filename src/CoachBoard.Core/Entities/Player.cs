using CoachBoard.Core.Entities.Base;
using CoachBoard.Core.Enums;

namespace CoachBoard.Core.Entities;

public class Player : BaseEntity
{
    public Player(long teamId, string name, DateTime birthDate, DateTime joinedDate, int overall, int kitNumber,
        PlayerPosition position)
    {
        TeamId = teamId;
        Name = name;
        BirthDate = birthDate;
        JoinedDate = joinedDate;
        Overall = overall;
        KitNumber = kitNumber;
        Position = position;
        Fixtures = new List<Fixture>();
        Transfers = new List<Transfer>();
        Assists = new List<Assist>();
    }

    public long TeamId { get; private set; }
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime JoinedDate { get; private set; }
    public int Overall { get; private set; }
    public int KitNumber { get; private set; }
    public PlayerPosition Position { get; private set; }
    public List<Fixture> Fixtures { get; private set; }
    public List<Transfer> Transfers { get; private set; }
    public List<Assist> Assists { get; private set; }
}