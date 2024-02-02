namespace CoachBoard.Domain.Shared;
public class Result
{
    protected Result(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; private set; }
    public Error? Error { get; private set; }

    public static Result Success() =>
        new(true);

    public static Result Failure(Error error) =>
        new(false, error);

    public static Result<TValue> Success<TValue>(TValue data) =>
        new(true, data);

    public static Result<TValue> Failure<TValue>(Error error) =>
        new(false, default, error);
}
