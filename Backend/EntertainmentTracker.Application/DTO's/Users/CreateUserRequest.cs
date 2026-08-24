namespace EntertainmentTracker.Application.DTOs.Users
{
    public sealed record CreateUserRequest(
        string Username,
        string Email,
        string Password);
}