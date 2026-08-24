namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class JwtSecuritySettings
    {
        public string Issuer { get; init; } = string.Empty;

        public string Audience { get; init; } = string.Empty;

        public int ExpirationMinutes { get; init; }
    }
}