using CoachBoard.Domain.Entities;

namespace CoachBoard.Application.Features.Users.Queries;
public record UserResponse(
    Guid Id,
    string Username,
    string Email,
    IReadOnlyCollection<Career> Careers);
