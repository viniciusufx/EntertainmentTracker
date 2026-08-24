using EntertainmentTracker.Domain.Exceptions;

namespace EntertainmentTracker.Domain.ValueObjects
{
    public sealed record RefreshTokenHash
    {
        public string Value { get; }

        private RefreshTokenHash(string value)
        {
            Value = value;
        }

        public static RefreshTokenHash Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidRefreshTokenHashException(
                    "Refresh token hash cannot be empty.");
            }

            return new RefreshTokenHash(value);
        }
    }
}