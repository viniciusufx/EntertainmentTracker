namespace EntertainmentTracker.Domain.Exceptions;

public sealed class UsernameAlreadyRegisteredException : DomainException
{
    public UsernameAlreadyRegisteredException()
        : base("Username is already registered.")
    {
    }
}