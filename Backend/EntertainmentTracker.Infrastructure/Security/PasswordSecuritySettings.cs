namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class PasswordSecuritySettings
    {
        public string Pepper { get; init; } = string.Empty;

        public int MemorySize { get; init; }

        public int Iterations { get; init; }

        public int DegreeOfParallelism { get; init; }

        public int SaltSize { get; init; }

        public int HashSize { get; init; }
    }
}