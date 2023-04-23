namespace CoachBoard.Core.Entities.Base;

public abstract class BaseEntity
{
    public long Id { get; }
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public void Delete() => 
        IsDeleted = true;
}