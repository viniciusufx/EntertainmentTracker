using System.Net.Mail;
using EntertainmentTracker.Domain.Exceptions;

namespace EntertainmentTracker.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            Validate(value);

            var normalizedEmail = value.Trim().ToLowerInvariant();

            return new Email(normalizedEmail);
        }

        private static void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidEmailException("Email cannot be empty.");
            }

            try
            {
                var emailAddress = new MailAddress(value);

                if (emailAddress.Address != value)
                {
                    throw new InvalidEmailException("Email must be valid.");
                }
            }
            catch (FormatException)
            {
                throw new InvalidEmailException("Email must be valid.");
            }
        }
    }
}