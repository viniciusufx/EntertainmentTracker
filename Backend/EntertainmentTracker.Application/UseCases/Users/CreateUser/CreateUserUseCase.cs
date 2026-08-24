using EntertainmentTracker.Application.Abstractions.Security;
using EntertainmentTracker.Application.DTOs.Users;
using EntertainmentTracker.Domain.Entities;
using EntertainmentTracker.Domain.Repositories;
using EntertainmentTracker.Domain.ValueObjects;
using EntertainmentTracker.Domain.Exceptions;

namespace EntertainmentTracker.Application.UseCases.Users.CreateUser
{
    public sealed class CreateUserUseCase
    {
        private readonly IUserRepository userRepository;
        private readonly IPasswordHasher passwordHasher;

        public CreateUserUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public async Task<UserResponse> ExecuteAsync(
            CreateUserRequest request,
            CancellationToken cancellationToken)
        {
            var username = Username.Create(request.Username);
            var email = Email.Create(request.Email);
            var password = Password.Create(request.Password);

            await ValidateUniquenessAsync(
                username,
                email,
                cancellationToken);

            var passwordHash = passwordHasher.Hash(password);
            var user = User.Create(username, email, passwordHash);

            await userRepository.AddAsync(user, cancellationToken);

            return MapToResponse(user);
        }

        private async Task ValidateUniquenessAsync(
            Username username,
            Email email,
            CancellationToken cancellationToken)
        {
            if (await userRepository.ExistsByUsernameAsync(
                username,
                cancellationToken))
            {
                throw new UsernameAlreadyRegisteredException();
            }

            if (await userRepository.ExistsByEmailAsync(
                email,
                cancellationToken))
            {
                throw new EmailAlreadyRegisteredException();
            }
        }

        private static UserResponse MapToResponse(User user)
        {
            return new UserResponse(
                user.Id.Value,
                user.Username.Value,
                user.Email.Value,
                user.Role,
                user.CreatedAt,
                user.UpdatedAt);
        }
    }
}