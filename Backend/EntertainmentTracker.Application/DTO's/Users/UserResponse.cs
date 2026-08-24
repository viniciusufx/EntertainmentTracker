using EntertainmentTracker.Domain.Enums;

namespace EntertainmentTracker.Application.DTOs.Users
{
    public sealed record UserResponse(
        Guid Id,
        string Username,
        string Email,
        UserRole Role,
        DateTime CreatedAt,
        DateTime? UpdatedAt);
}