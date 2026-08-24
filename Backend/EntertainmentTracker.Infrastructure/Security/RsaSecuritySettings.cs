namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class RsaSecuritySettings
    {
        public string PrivateKey { get; init; } = string.Empty;

        public string PublicKey { get; init; } = string.Empty;
    }
}