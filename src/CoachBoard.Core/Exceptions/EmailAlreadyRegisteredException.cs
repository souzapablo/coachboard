using System.Net;

namespace CoachBoard.Core.Exceptions;

public class EmailAlreadyRegisteredException : BaseException
{
    public EmailAlreadyRegisteredException()
        : base("E-mail already registered", HttpStatusCode.BadRequest)
    {
    }
}