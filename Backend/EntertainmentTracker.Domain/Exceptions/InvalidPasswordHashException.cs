namespace EntertainmentTracker.Domain.Exceptions;

public sealed class InvalidPasswordHashException : DomainException
{
    public InvalidPasswordHashException(string message)
        : base(message)
    {
    }
}