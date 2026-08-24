namespace EntertainmentTracker.Domain.Exceptions;

public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException(string message)
        : base(message)
    {
    }
}