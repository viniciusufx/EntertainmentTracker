using System.Security.Cryptography;
using System.Text;
using EntertainmentTracker.Application.Abstractions.Security;

namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class RefreshTokenGenerator
        : IRefreshTokenGenerator
    {
        private const int TokenSize = 32;

        public GeneratedRefreshToken Generate()
        {
            var token = GenerateToken();
            var hash = GenerateHash(token);

            return new GeneratedRefreshToken(
                token,
                hash);
        }

        private static string GenerateToken()
        {
            var token = RandomNumberGenerator.GetBytes(TokenSize);

            return Convert.ToBase64String(token)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private static string GenerateHash(string token)
        {
            var tokenBytes = Encoding.UTF8.GetBytes(token);
            var hash = SHA256.HashData(tokenBytes);

            return Convert.ToHexString(hash);
        }
    }
}