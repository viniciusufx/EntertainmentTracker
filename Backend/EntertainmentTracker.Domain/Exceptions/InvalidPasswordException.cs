namespace EntertainmentTracker.Domain.Exceptions;

public sealed class InvalidPasswordException : DomainException
{
    public InvalidPasswordException(string message)
        : base(message)
    {
    }
}