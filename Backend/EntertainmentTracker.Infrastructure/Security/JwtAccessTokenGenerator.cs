using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EntertainmentTracker.Application.Abstractions.Security;
using EntertainmentTracker.Domain.Entities;

namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class JwtAccessTokenGenerator
        : IAccessTokenGenerator
    {
        private readonly JwtSecuritySettings settings;
        private readonly RsaKeyProvider rsaKeyProvider;

        public JwtAccessTokenGenerator(
            IOptions<JwtSecuritySettings> options,
            RsaKeyProvider rsaKeyProvider)
        {
            settings = options.Value;
            this.rsaKeyProvider = rsaKeyProvider;
        }

        public string Generate(User user)
        {
            var claims = CreateClaims(user);
            var credentials = CreateSigningCredentials();
            var token = CreateToken(claims, credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static List<Claim> CreateClaims(User user)
        {
            return
            [
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.Value.ToString()),

                new Claim(
                    JwtRegisteredClaimNames.UniqueName,
                    user.Username.Value),

                new Claim(
                    ClaimTypes.Role,
                    user.Role.ToString())
            ];
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(new RsaSecurityKey(rsaKeyProvider.GetPrivateKey()), SecurityAlgorithms.RsaSha256);
        }

        private JwtSecurityToken CreateToken(
            List<Claim> claims,
            SigningCredentials credentials)
        {
            return new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    settings.ExpirationMinutes),
                signingCredentials: credentials);
        }
    }
}