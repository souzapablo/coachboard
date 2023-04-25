namespace CoachBoard.Core.Exceptions;

public class EntityNotFoundException<T> : Exception
{
    public EntityNotFoundException(long id)
        : base($"{typeof(T).Name} with id [{id}] not found")
    {
    }
}