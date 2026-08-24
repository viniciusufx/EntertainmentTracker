using EntertainmentTracker.Domain.Entities;
using EntertainmentTracker.Domain.ValueObjects;

namespace EntertainmentTracker.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByHashAsync(
            RefreshTokenHash refreshTokenHash,
            CancellationToken cancellationToken);

        Task AddAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken);

        Task UpdateAsync(
            RefreshToken refreshToken,
            CancellationToken cancellationToken);
    }
}