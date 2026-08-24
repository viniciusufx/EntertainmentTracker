using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using EntertainmentTracker.Application.Abstractions.Security;
using EntertainmentTracker.Domain.Exceptions;
using EntertainmentTracker.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class Argon2PasswordHasher : IPasswordHasher
    {
        private const string Algorithm = "argon2id";
        private const string Version = "v=19";

        private readonly PasswordSecuritySettings settings;

        public Argon2PasswordHasher(
            IOptions<PasswordSecuritySettings> options)
        {
            settings = options.Value;
            ValidateSettings();
        }

        public PasswordHash Hash(Password password)
        {
            var salt = RandomNumberGenerator.GetBytes(
                settings.SaltSize);

            var hash = ComputeHash(
                password.Value,
                salt,
                settings.MemorySize,
                settings.Iterations,
                settings.DegreeOfParallelism);

            return PasswordHash.Create(
                BuildEncodedHash(salt, hash));
        }

        public bool Verify(
            Password password,
            PasswordHash passwordHash)
        {
            var encodedHash = ParseHash(passwordHash.Value);

            var computedHash = ComputeHash(
                password.Value,
                encodedHash.Salt,
                encodedHash.MemorySize,
                encodedHash.Iterations,
                encodedHash.DegreeOfParallelism);

            return CryptographicOperations.FixedTimeEquals(
                computedHash,
                encodedHash.Hash);
        }

        public bool NeedsRehash(PasswordHash passwordHash)
        {
            var encodedHash = ParseHash(passwordHash.Value);

            return encodedHash.MemorySize != settings.MemorySize
                || encodedHash.Iterations != settings.Iterations
                || encodedHash.DegreeOfParallelism != settings.DegreeOfParallelism;
        }

        private byte[] ComputeHash(
            string password,
            byte[] salt,
            int memorySize,
            int iterations,
            int degreeOfParallelism)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(
                password + settings.Pepper);

            using var argon2 = new Argon2id(passwordBytes);

            argon2.Salt = salt;
            argon2.MemorySize = memorySize;
            argon2.Iterations = iterations;
            argon2.DegreeOfParallelism = degreeOfParallelism;

            return argon2.GetBytes(settings.HashSize);
        }

        private string BuildEncodedHash(
            byte[] salt,
            byte[] hash)
        {
            return string.Join(
                '$',
                Algorithm,
                Version,
                $"m={settings.MemorySize},t={settings.Iterations},p={settings.DegreeOfParallelism}",
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        private static EncodedHash ParseHash(string value)
        {
            var parts = value.Split('$');

            if (parts.Length != 6
                || parts[0] != string.Empty
                || parts[1] != Algorithm
                || parts[2] != Version)
            {
                throw new InvalidPasswordHashException(
                    "Invalid Argon2id password hash format.");
            }

            var parameters = ParseParameters(parts[3]);

            return new EncodedHash(
                parameters.MemorySize,
                parameters.Iterations,
                parameters.DegreeOfParallelism,
                Convert.FromBase64String(parts[4]),
                Convert.FromBase64String(parts[5]));
        }

        private static Argon2Parameters ParseParameters(
            string value)
        {
            var parameters = value.Split(',');

            if (parameters.Length != 3)
            {
                throw new InvalidPasswordHashException(
                    "Invalid Argon2id parameters.");
            }

            return new Argon2Parameters(
                ParseParameter(parameters[0], "m"),
                ParseParameter(parameters[1], "t"),
                ParseParameter(parameters[2], "p"));
        }

        private static int ParseParameter(
            string value,
            string expectedName)
        {
            var parts = value.Split('=');

            if (parts.Length != 2
                || parts[0] != expectedName
                || !int.TryParse(parts[1], out var result)
                || result <= 0)
            {
                throw new InvalidPasswordHashException(
                    "Invalid Argon2id parameter.");
            }

            return result;
        }

        private void ValidateSettings()
        {
            if (string.IsNullOrWhiteSpace(settings.Pepper))
            {
                throw new InvalidOperationException(
                    "Password pepper is not configured.");
            }

            if (settings.MemorySize <= 0
                || settings.Iterations <= 0
                || settings.DegreeOfParallelism <= 0
                || settings.SaltSize <= 0
                || settings.HashSize <= 0)
            {
                throw new InvalidOperationException(
                    "Invalid Argon2id security configuration.");
            }
        }

        private sealed record EncodedHash(
            int MemorySize,
            int Iterations,
            int DegreeOfParallelism,
            byte[] Salt,
            byte[] Hash);

        private sealed record Argon2Parameters(
            int MemorySize,
            int Iterations,
            int DegreeOfParallelism);
    }
}