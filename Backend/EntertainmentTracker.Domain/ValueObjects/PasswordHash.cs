using EntertainmentTracker.Domain.Exceptions;

namespace EntertainmentTracker.Domain.ValueObjects
{
    public sealed record PasswordHash
    {
        public string Value { get; }

        private PasswordHash(string value)
        {
            Value = value;
        }

        public static PasswordHash Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidPasswordHashException("Password hash cannot be empty.");
            }

            return new PasswordHash(value);
        }
    }
}