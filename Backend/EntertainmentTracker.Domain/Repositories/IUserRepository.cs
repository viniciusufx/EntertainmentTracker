using EntertainmentTracker.Domain.Entities;
using EntertainmentTracker.Domain.ValueObjects;

namespace EntertainmentTracker.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(
            UserId userId,
            CancellationToken cancellationToken);

        Task<User?> GetByEmailAsync(
            Email email,
            CancellationToken cancellationToken);

        Task<User?> GetByUsernameAsync(
            Username username,
            CancellationToken cancellationToken);

        Task AddAsync(
            User user,
            CancellationToken cancellationToken);

        Task<bool> ExistsByEmailAsync(
            Email email,
            CancellationToken cancellationToken);

        Task<bool> ExistsByUsernameAsync(
            Username username,
            CancellationToken cancellationToken);
    }
}