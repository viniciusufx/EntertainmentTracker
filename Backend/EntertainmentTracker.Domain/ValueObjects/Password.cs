using EntertainmentTracker.Domain.Exceptions;

namespace EntertainmentTracker.Domain.ValueObjects
{
    public sealed record Password
    {
        public string Value { get; }

        private Password(string value)
        {
            Value = value;
        }

        public static Password Create(string value)
        {
            Validate(value);

            return new Password(value);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidPasswordException("Password cannot be empty.");
            }

            if (value.Length < 10 || value.Length > 18)
            {
                throw new InvalidPasswordException("Password must contain between 10 and 18 characters.");
            }

            ValidateWhitespace(value);
            ValidateUppercase(value);
            ValidateLowercase(value);
            ValidateNumber(value);
            ValidateSpecialCharacter(value);
        }

        private static void ValidateWhitespace(string value)
        {
            if (value.Any(char.IsWhiteSpace))
            {
                throw new InvalidPasswordException("Password cannot contain whitespace.");
            }
        }

        private static void ValidateUppercase(string value)
        {
            if (!value.Any(char.IsUpper))
            {
                throw new InvalidPasswordException("Password must contain at least one uppercase letter.");
            }
        }

        private static void ValidateLowercase(string value)
        {
            if (!value.Any(char.IsLower))
            {
                throw new InvalidPasswordException("Password must contain at least one lowercase letter.");
            }
        }

        private static void ValidateNumber(string value)
        {
            if (!value.Any(char.IsDigit))
            {
                throw new InvalidPasswordException("Password must contain at least one number.");
            }
        }

        private static void ValidateSpecialCharacter(string value)
        {
            if (!value.Any(IsSpecialCharacter))
            {
                throw new InvalidPasswordException("Password must contain at least one special character.");
            }
        }

        private static bool IsSpecialCharacter(char character)
        {
            return !char.IsLetterOrDigit(character);
        }
    }
}