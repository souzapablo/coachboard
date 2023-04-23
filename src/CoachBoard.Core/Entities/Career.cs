using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Career : BaseEntity
{
    public Career(long userId, string managerName)
    {
        UserId = userId;
        ManagerName = managerName;
        Teams = new List<Team>();
    }

    public long UserId { get; private set; }
    public string ManagerName { get; private set; }
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public List<Team> Teams { get; private set; }
}