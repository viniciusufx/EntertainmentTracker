using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace EntertainmentTracker.Infrastructure.Security
{
    public sealed class RsaKeyProvider
    {
        private readonly RSA privateKey;
        private readonly RSA publicKey;

        public RsaKeyProvider(
            IOptions<RsaSecuritySettings> options)
        {
            var settings = options.Value;

            privateKey = CreatePrivateKey(settings.PrivateKey);
            publicKey = CreatePublicKey(settings.PublicKey);
        }

        public RSA GetPrivateKey()
        {
            return privateKey;
        }

        public RSA GetPublicKey()
        {
            return publicKey;
        }

        private static RSA CreatePrivateKey(string pem)
        {
            var key = RSA.Create();

            key.ImportFromPem(pem);

            return key;
        }

        private static RSA CreatePublicKey(string pem)
        {
            var key = RSA.Create();

            key.ImportFromPem(pem);

            return key;
        }
    }
}