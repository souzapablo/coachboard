using CoachBoard.Domain.Shared;

namespace CoachBoard.Domain.Errors;
public static class DomainErrors
{
    public static class User
    {
        public static Error RegisteredEmail => new("RegisteredEmail", "The email provided has already been registered.");
        public static Error UserNotFound(Guid id) => new("UserNotFound", $"User with id {id} not found.");
    }

    public static class Auth
    {
        public static Error InvalidCredentials => new("InvalidCredentials", "The credentials provided are invalid.");
    }
}
