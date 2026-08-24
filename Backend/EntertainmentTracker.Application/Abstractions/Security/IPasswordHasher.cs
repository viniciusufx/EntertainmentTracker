using EntertainmentTracker.Domain.ValueObjects;

namespace EntertainmentTracker.Application.Abstractions.Security
{
    public interface IPasswordHasher
    {
        PasswordHash Hash(Password password);

        bool Verify(
            Password password,
            PasswordHash passwordHash);

        bool NeedsRehash(PasswordHash passwordHash);
    }
}