using EntertainmentTracker.Domain.Exceptions;

namespace EntertainmentTracker.Domain.ValueObjects
{
    public sealed record Username
    {
        public string Value { get; }

        private Username(string value)
        {
            Value = value;
        }

        public static Username Create(string value)
        {
            Validate(value);

            return new Username(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidUsernameException("Username cannot be empty.");
            }

            if (value.Any(char.IsWhiteSpace))
            {
                throw new InvalidUsernameException("Username cannot contain whitespace.");
            }

            if (value.Length < 6 || value.Length > 30)
            {
                throw new InvalidUsernameException("Username must contain between 6 and 30 characters.");
            }
        }
    }
}