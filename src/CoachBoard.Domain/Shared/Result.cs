namespace CoachBoard.Domain.Shared;
public class Result
{
    protected Result()
    {
        IsSuccess = true;
    }

    protected Result(Error error)
    {
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public Error? Error { get; private set; }

    public static Result Success =>
        new();

    public static Result Failure(Error error) =>
        new(error);
}
