using CoachBoard.Core.Entities.Base;

namespace CoachBoard.Core.Entities;

public class Career : BaseEntity
{
    public Career(string managerName)
    {
        ManagerName = managerName;
        Teams = new List<Team>();
    }

    public string ManagerName { get; private set; }
    public DateTime LastUpdate { get; private set; } = DateTime.UtcNow;
    public List<Team> Teams { get; private set; }
}