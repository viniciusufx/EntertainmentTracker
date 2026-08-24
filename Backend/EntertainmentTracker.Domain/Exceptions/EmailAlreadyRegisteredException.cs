namespace EntertainmentTracker.Domain.Exceptions;

public sealed class EmailAlreadyRegisteredException : DomainException
{
    public EmailAlreadyRegisteredException()
        : base("Email is already registered.")
    {
    }
}