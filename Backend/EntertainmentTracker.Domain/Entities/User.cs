using EntertainmentTracker.Domain.Enums;
using EntertainmentTracker.Domain.ValueObjects;

namespace EntertainmentTracker.Domain.Entities
{
    public sealed class User
    {
        public UserId Id { get; }

        public Username Username { get; private set; }

        public Email Email { get; private set; }

        public PasswordHash PasswordHash { get; private set; }

        public UserRole Role { get; private set; }

        public DateTime CreatedAt { get; }

        public DateTime? UpdatedAt { get; private set; }

        private User(
            UserId id,
            Username username,
            Email email,
            PasswordHash passwordHash,
            UserRole role)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(
            Username username,
            Email email,
            PasswordHash passwordHash)
        {
            return new User(
                UserId.Create(),
                username,
                email,
                passwordHash,
                UserRole.User);
        }

        public void ChangeUsername(Username username)
        {
            Username = username;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeEmail(Email email)
        {
            Email = email;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(PasswordHash passwordHash)
        {
            PasswordHash = passwordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeRole(UserRole role)
        {
            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}