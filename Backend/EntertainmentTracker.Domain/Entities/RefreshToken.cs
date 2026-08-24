using EntertainmentTracker.Domain.ValueObjects;

namespace EntertainmentTracker.Domain.Entities
{
    public sealed class RefreshToken
    {
        public RefreshTokenId Id { get; }

        public UserId UserId { get; }

        public RefreshTokenHash TokenHash { get; }

        public DateTime CreatedAt { get; }

        public DateTime ExpiresAt { get; }

        public DateTime? RevokedAt { get; private set; }

        public RefreshTokenId? ReplacedByTokenId { get; private set; }

        private RefreshToken(
            RefreshTokenId id,
            UserId userId,
            RefreshTokenHash tokenHash,
            DateTime createdAt,
            DateTime expiresAt)
        {
            Id = id;
            UserId = userId;
            TokenHash = tokenHash;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
        }

        public static RefreshToken Create(
            UserId userId,
            RefreshTokenHash tokenHash,
            DateTime createdAt,
            DateTime expiresAt)
        {
            return new RefreshToken(
                RefreshTokenId.Create(),
                userId,
                tokenHash,
                createdAt,
                expiresAt);
        }

        public void Revoke(DateTime revokedAt)
        {
            RevokedAt = revokedAt;
        }

        public void Replace(
            RefreshTokenId replacementTokenId,
            DateTime revokedAt)
        {
            ReplacedByTokenId = replacementTokenId;
            RevokedAt = revokedAt;
        }
    }
}