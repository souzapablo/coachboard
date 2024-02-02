namespace CoachBoard.Domain.Shared;
public class Result<TValue> : Result
{
    private Result(TValue data)
        : base()
    {
        Data = data;
    }

    private Result(Error error)
    : base(error)
    {

    }

    public TValue? Data { get; private set; }

    public static new Result<TValue> Success(TValue data) =>
        new(data);

    public static new Result<TValue> Failure(Error error) =>
        new(error);
}
