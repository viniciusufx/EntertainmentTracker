namespace EntertainmentTracker.Application.Abstractions.Security
{
    public interface IRefreshTokenGenerator
    {
        GeneratedRefreshToken Generate();
    }
}