using Microsoft.Extensions.Options;

namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class PasswordSecuritySettingsValidator
        : IValidateOptions<PasswordSecuritySettings>
    {
        public ValidateOptionsResult Validate(
            string? name,
            PasswordSecuritySettings settings)
        {
            var errors = GetValidationErrors(settings);

            return errors.Count == 0
                ? ValidateOptionsResult.Success
                : ValidateOptionsResult.Fail(errors);
        }

        private static List<string> GetValidationErrors(
            PasswordSecuritySettings settings)
        {
            var errors = new List<string>();

            AddErrorIf(
                errors,
                string.IsNullOrWhiteSpace(settings.Pepper),
                "Pepper cannot be empty.");

            AddErrorIf(
                errors,
                settings.MemorySize <= 0,
                "Memory size must be greater than zero.");

            AddErrorIf(
                errors,
                settings.Iterations <= 0,
                "Iterations must be greater than zero.");

            AddErrorIf(
                errors,
                settings.DegreeOfParallelism <= 0,
                "Degree of parallelism must be greater than zero.");

            AddErrorIf(
                errors,
                settings.SaltSize <= 0,
                "Salt size must be greater than zero.");

            AddErrorIf(
                errors,
                settings.HashSize <= 0,
                "Hash size must be greater than zero.");

            return errors;
        }

        private static void AddErrorIf(
            ICollection<string> errors,
            bool condition,
            string message)
        {
            if (condition)
            {
                errors.Add(message);
            }
        }
    }
}