using CoachBoard.Domain.Shared;

namespace CoachBoard.Domain.Errors;
public static class DomainErrors
{
    public static class User
    {
        public static Error RegisteredEmail => new("RegisteredEmail", "The email provided has already been registered.");
    }
}
