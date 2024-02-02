namespace CoachBoard.Domain.Shared;
public class Result<TValue> : Result
{
    public Result(bool isSuccess, TValue? data, Error? error = null)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public TValue? Data { get; private set; }
}
