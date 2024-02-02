namespace CoachBoard.Domain.Entities.Base;
public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public void Delete() =>
        IsDeleted = true;
}
