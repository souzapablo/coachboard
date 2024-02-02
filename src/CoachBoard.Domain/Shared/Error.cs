namespace CoachBoard.Domain.Shared;
public class Error(string code, string message)
{
    public string Code { get; private set; } = code;
    public string Message { get; private set;} = message;

    public override string ToString()
    {
        return Code;
    }
}
